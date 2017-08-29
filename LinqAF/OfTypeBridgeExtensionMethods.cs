using LinqAF.Impl;

namespace LinqAF
{
    public static class OfTypeBridgeExtensionMethods
    {
        public static OfTypeEnumerable<object, TOutItem, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator> OfType<TOutItem>(this System.Collections.IEnumerable source)
        {
            var bridge = CommonImplementation.Bridge(source, nameof(source));

            return CommonImplementation.OfTypeImpl<object, TOutItem, IdentityEnumerable<object, System.Collections.IEnumerable, IEnumerableBridger, IdentityEnumerator>, IdentityEnumerator>(ref bridge);
        }
    }
}
