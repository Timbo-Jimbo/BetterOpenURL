#if UNITY_ANDROID

using UnityEditor.Android;
using UnityEngine;

namespace TimboJimbo.BetterOpenURL.Editor.Android
{
    public class AddQueriesIntentToManifest : IPostGenerateGradleAndroidProject
    {
        public int callbackOrder => 0;

        public void OnPostGenerateGradleAndroidProject(string path)
        {
            const string AndroidXmlNamespace = "http://schemas.android.com/apk/res/android";

            // We need to add the following to detect if the CustomTabsService is available on the device, so we 
            // know when we need to fallback to Application.OpenURL...!
            // <manifest>
            //    <queries>
            //        <intent>
            //            <action android:name="android.support.customtabs.action.CustomTabsService" />
            //        </intent>
            //    </queries>
            // </manifest>

            string manifestPath = path + "/src/main/AndroidManifest.xml";

            var doc = new System.Xml.XmlDocument();
            doc.Load(manifestPath);
            
            var namespaceManager = new System.Xml.XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace("android", AndroidXmlNamespace);
            
            //try find existing better-open-url-query-root
            {
                var betterOpenUrlQueryRoot = doc.SelectSingleNode("/manifest/queries[@android:name='better-open-url-query-root']", namespaceManager);
                
                //remove it and the comment right before it
                var parent = betterOpenUrlQueryRoot?.ParentNode;
                
                if(betterOpenUrlQueryRoot?.PreviousSibling?.NodeType == System.Xml.XmlNodeType.Comment)
                    parent?.RemoveChild(betterOpenUrlQueryRoot.PreviousSibling);
                parent?.RemoveChild(betterOpenUrlQueryRoot);
            }

            var manifest = doc.SelectSingleNode("/manifest");
            if(manifest == null) throw new System.Exception("Could not find manifest node in AndroidManifest.xml");
            {
                var comment = doc.CreateComment("BetterOpenURL: Required to detect if CustomTabsService is available on the device");
                manifest.AppendChild(comment);

                var queries = doc.CreateElement("queries");
                {
                    var nameAttribute = doc.CreateAttribute("android", "name", AndroidXmlNamespace);
                    nameAttribute.Value = "better-open-url-query-root";
                    queries.Attributes.Append(nameAttribute);
                    
                    var intent = doc.CreateElement("intent");
                    {
                        var action = doc.CreateElement("action");
                        {
                            var actionAttribute = doc.CreateAttribute("android", "name", AndroidXmlNamespace);
                            actionAttribute.Value = "android.support.customtabs.action.CustomTabsService";
                            action.Attributes.Append(actionAttribute);
                        }
                        intent.AppendChild(action);
                    }
                    queries.AppendChild(intent);
                }
                manifest.AppendChild(queries);
            }

            doc.Save(manifestPath);
        }
    }
}
#endif