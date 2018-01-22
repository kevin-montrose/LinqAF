using System;

namespace LinqAF
{
    abstract class TemplateBase
    {
        // rewritten as `This()`
        protected abstract dynamic This();
        
        // rewritten as `ref this`
        protected abstract dynamic RefThis();

        // rewritten as `this.IsDefaultValue()`
        protected abstract bool IsDefaultValue();
    }
}
