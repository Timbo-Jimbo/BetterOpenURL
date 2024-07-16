using UnityEngine;

namespace TimboJimbo.BetterOpenURL
{
    internal class ShowIfAttribute : PropertyAttribute
    {
        public readonly string ConditionalSourceField;

        public ShowIfAttribute(string conditionalSourceField)
        {
            ConditionalSourceField = conditionalSourceField;
        }
    }
}