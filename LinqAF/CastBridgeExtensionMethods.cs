using LinqAF.Impl;

namespace LinqAF
{
    public static class CastBridgeExtensionMethods
    {
        public static CastEnumerable<object, TOutItem, IdentityEnumerable<object, System.Collections.IEnumerable, IdentityEnumerator>, IdentityEnumerator> Cast<TOutItem>(this System.Collections.IEnumerable source)
        {
            var bridge = CommonImplementation.Bridge(source, nameof(source));

            return CommonImplementation.CastImpl<object, TOutItem, IdentityEnumerable<object, System.Collections.IEnumerable, IdentityEnumerator>, IdentityEnumerator>(ref bridge);
        }
    }
}
