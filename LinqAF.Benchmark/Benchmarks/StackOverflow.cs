using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LinqAF.Benchmark.Benchmarks
{
    public class StackOverflow
    {
        static readonly ShowViewData VD = MakeShowViewData();

        class ShowViewData
        {
            public User CurrentUser { get; set; }
            public IEnumerable<Post> Answers { get; set; }
        }

        class User
        {
            public bool CanSeePost(Post p) => p.Id % 2 == 0;
        }

        class Post
        {
            public int Id { get; set; }
            public bool IsLocked { get; set; }
        }

        static ShowViewData MakeShowViewData()
        {
            var ret = new ShowViewData();
            var answers = new List<Post>();
            for(var i = 0; i < 5; i++)
            {
                answers.Add(new Post { Id = i + 1, IsLocked = i % 3 == 0 });
            }

            ret.Answers = answers;
            ret.CurrentUser = new User();
            return ret;
        }

        public class QuestionShow
        {
            [Benchmark]
            public void LinqAF()
            {
                var vd = VD;

                var res = vd.Answers.Where(a => a.IsLocked && vd.CurrentUser.CanSeePost(a)).Select(a => a.Id).ToList();

                GC.KeepAlive(res);
            }

            [Benchmark(Baseline =true)]
            public void LINQ2Objects()
            {
                var vd = VD;

                var res =
                    System.Linq.Enumerable.ToList(
                        System.Linq.Enumerable.Select(
                            System.Linq.Enumerable.Where(vd.Answers, a => a.IsLocked && vd.CurrentUser.CanSeePost(a)),
                            a => a.Id
                        )
                    );

                GC.KeepAlive(res);
            }
        }

        public class Interesting
        {
            static readonly List<dynamic> BeforeQs = MakeBeforeQs();
            static readonly List<dynamic> AfterQs = MakeAfterQs();

            static readonly Func<dynamic, string[]> Func1 = q => (string[])Tag_ToArray(q._Tags);
            static readonly Func<string, string> Func2 = t => t;
            static readonly Func<GroupingEnumerable<string, string>, string> Func3_LAF = d => d.Key;
            static readonly Func<System.Linq.IGrouping<string, string>, string> Func3_L2O = d => d.Key;
            static readonly Func<GroupingEnumerable<string, string>, float> Func4_LAF = d => ((float)d.Count()) / (float)BeforeQs.Count;
            static readonly Func<System.Linq.IGrouping<string, string>, float> Func4_L2O = d => ((float)d.Count()) / (float)BeforeQs.Count;


            static List<dynamic> MakeBeforeQs() => new List<dynamic> { new { _Tags = "c#" }, new { _Tags = "c# .net" }, new { _Tags = "java" }, new { _Tags = "java android" }, new { _Tags = "javascript jquery" } };
            static List<dynamic> MakeAfterQs() => new List<dynamic> { new { _Tags = "ios swift" }, new { _Tags = "r" }, new { _Tags = "python django" } };

            [Benchmark]
            public void LinqAF()
            {
                var before =
                    BeforeQs
                        .SelectMany(Func1)
                        .GroupBy(Func2)
                        .ToDictionary(
                            Func3_LAF,
                            Func4_LAF
                        );

                var after =
                    AfterQs
                        .SelectMany(Func1)
                        .GroupBy(Func2)
                        .ToDictionary(
                            Func3_LAF,
                            Func4_LAF
                        );
            }

            [Benchmark(Baseline = true)]
            public void LINQ2Objects()
            {
                var before =
                    System.Linq.Enumerable.ToDictionary(
                        System.Linq.Enumerable.GroupBy(
                            System.Linq.Enumerable.SelectMany(
                                BeforeQs,
                                Func1
                            ),
                            Func2
                        ),
                        Func3_L2O,
                        Func4_L2O
                    );

                var after =
                    System.Linq.Enumerable.ToDictionary(
                        System.Linq.Enumerable.GroupBy(
                            System.Linq.Enumerable.SelectMany(
                                AfterQs,
                                Func1
                            ),
                            Func2
                        ),
                        Func3_L2O,
                        Func4_L2O
                    );
            }

            static string[] Tag_ToArray(string tags, bool allowPlusDelimited = true, bool unicodeTags = false)
            {
                // return an empty array if no string was passed
                if (string.IsNullOrEmpty(tags)) return new string[] { };

                tags = tags.Trim();

                // decode from database if necessary
                switch (GetEncodingFormat(tags))
                {
#pragma warning disable 0618
                    case TagEncodingFormat.LegacyFullTextIndex:
#pragma warning restore 0618
                        tags = DecodeFromDb(tags);
                        // no need to test for anything here; assume it was direct from db 
                        // and thus properly encoded
                        return tags.Split(StringSplits.Space, StringSplitOptions.RemoveEmptyEntries);
                    case TagEncodingFormat.PipeDelimited:
                        var arr = tags.Split(StringSplits.VerticalBar, StringSplitOptions.RemoveEmptyEntries);
                        return arr;
                }

                // handle either space or plus (web URL) delimited            
                if (allowPlusDelimited && IsPlusDelimited(tags, unicodeTags))
                    return tags.Split(StringSplits.Plus, StringSplitOptions.RemoveEmptyEntries);
                else
                    return RemoveExtraSpaces(tags).Split(StringSplits.Space, StringSplitOptions.RemoveEmptyEntries);
            }

            enum TagEncodingFormat : byte
            {
                None,
                [Obsolete("I will cut you...")]
                LegacyFullTextIndex,
                PipeDelimited
            }
            static TagEncodingFormat GetEncodingFormat(string tag)
            {
                if (string.IsNullOrEmpty(tag)) return TagEncodingFormat.None;
                tag = tag.Trim();

                if (tag.StartsWith("|") && tag.EndsWith("|")) return TagEncodingFormat.PipeDelimited;
#pragma warning disable 0618
                if (tag.StartsWith("é") && tag.EndsWith("à")) return TagEncodingFormat.LegacyFullTextIndex;
#pragma warning restore 0618
                return TagEncodingFormat.None;
            }

            static readonly Regex _PlusDelimitedUnicode = new Regex(@"[\p{L}\p{M}0-9#\-\.]\+[\p{L}\p{M}0-9#\-\.]", RegexOptions.Compiled);
            static readonly Regex _PlusDelimitedAscii = new Regex(@"[a-z0-9#\-\.]\+[a-z0-9#\-\.]", RegexOptions.Compiled);
            static bool IsPlusDelimited(string tags, bool unicodeTags = false)
            {
                if (string.IsNullOrEmpty(tags)) return false;
                if (tags.Contains(" ")) return false;
                var re = unicodeTags ? _PlusDelimitedUnicode : _PlusDelimitedAscii;
                return re.IsMatch(tags);
            }

            static string DecodeFromDb(string tag)
            {
                if (string.IsNullOrWhiteSpace(tag) || tag == "|" || tag == "||")
                    return "";

                // don't do this if it isn't actually encoded for the db
                var enc = GetEncodingFormat(tag);

                if (enc == TagEncodingFormat.None) return tag;

                switch (enc)
                {
#pragma warning disable 0618
                    case TagEncodingFormat.LegacyFullTextIndex:
#pragma warning restore 0618
                        int l = tag.Length;

                        // pretty much a nasty hack, unless we can define our own SQL Server 2005 search languages
                        // the alternative is the SQL like %this% clause, which is unindexable!
                        var sb = new StringBuilder(1);
                        for (int i = 0; i < l; i++)
                        {
                            switch (tag[i])
                            {
                                case 'é':
                                    break;
                                case 'à':
                                    if (i != l - 1) sb.Append(' ');
                                    break;
                                case 'ñ':
                                    sb.Append('#');
                                    break;
                                case 'ç':
                                    sb.Append('+');
                                    break;
                                case 'ö':
                                    sb.Append('-');
                                    break;
                                case 'û':
                                    sb.Append('.');
                                    break;
                                case ' ':
                                    break;
                                default:
                                    sb.Append(tag[i]);
                                    break;
                            }
                        }
                        return sb.ToString();
                    case TagEncodingFormat.PipeDelimited:
                        return tag.Replace('|', ' ').Trim();
                    default:
                        return tag;
                }
            }

            static string RemoveExtraSpaces(string s)
            {
                if (string.IsNullOrEmpty(s)) return s;
                s = Regex.Replace(s, @"(\u200c|\s){2,}", " "); // see http://en.wikipedia.org/wiki/Zero-width_non-joiner
                s = Regex.Replace(s, @"(\p{Cc}|\p{Cf})\1+", "$1"); // any other duplicated funky-looking characters? replace them with one of.
                s = s.Trim();
                return s;
            }

            static class StringSplits
            {
                public static readonly char[] Space = { ' ' },
                                              Comma = { ',' },
                                              Period = { '.' },
                                              Minus = { '-' },
                                              Plus = { '+' },
                                              Asterisk = { '*' },
                                              Percent = { '%' },
                                              Ampersand = { '&' },
                                              Equal = { '=' },
                                              Underscore = { '_' },
                                              NewLine = { '\n' },
                                              SemiColon = { ';' },
                                              Colon = { ':' },
                                              VerticalBar = { '|' },
                                              ForwardSlash = { '/' },
                                              DoubleQuote = { '"' },
                                              Dash = { '-' },
                                              NewLine_CarriageReturn = { '\n', '\r' },
                                              Comma_Space = { ',', ' ' },
                                              Comma_SemiColon = { ',', ';' },
                                              Comma_SemiColon_Space = { ',', ';', ' ' },
                                              Comma_SemiColon_Space_CR_LF = { ',', ';', ' ', '\r', '\n' },
                                              BackSlash_Slash_Period = { '\\', '/', '.' },
                                              DoubleRightArrow = { '»' };

                public static readonly string[] CarriageReturnNewLineString = { "\r\n" };
            }
            }
        }
}
