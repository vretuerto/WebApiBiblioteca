using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebApiBiblioteca.Utils
{
  
        public class SwaggerVersion : IControllerModelConvention
        {
            public void Apply(ControllerModel controller)
            {
                var namespaceController = controller.ControllerType.Namespace; 
                var versionAPI = namespaceController.Split('.').Last().ToLower(); 
                controller.ApiExplorer.GroupName = versionAPI;
            }
        }
    }

