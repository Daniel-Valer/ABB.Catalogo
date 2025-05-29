using System;
using System.Reflection;

namespace ABB.Catalogo.WebServiceAbb.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}