using JetBrains.Annotations;

namespace TimboJimbo.BetterOpenURL.Auth
{
    public delegate void OnAuthResult(AuthURLResult result, [CanBeNull] string url);
}