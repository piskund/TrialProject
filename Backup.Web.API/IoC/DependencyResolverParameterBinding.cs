// -------------------------------------------------------------------------------------------------------------
//  DependencyResolverParameterBinding.cs created by DEP on 2017/01/13
// -------------------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace Backup.Web.API.IoC
{
    public sealed class DependencyResolverParameterBinding : HttpParameterBinding
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyResolverParameterBinding"/> class.
        /// </summary>
        /// <param name="descriptor">An <see cref="T:System.Web.Http.Controllers.HttpParameterDescriptor" /> that describes the parameters.</param>
        public DependencyResolverParameterBinding(HttpParameterDescriptor descriptor) : base(descriptor)
        {
        }

        /// <summary>
        /// Asynchronously executes the binding for the given request.
        /// </summary>
        /// <param name="metadataProvider">Metadata provider to use for validation.</param>
        /// <param name="actionContext">
        /// The action context for the binding. The action context contains the parameter dictionary that will get populated with the parameter.
        /// </param>
        /// <param name="cancellationToken">Cancellation token for cancelling the binding operation.</param>
        /// <returns>
        /// A task object representing the asynchronous operation.
        /// </returns>
        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            if (actionContext.ControllerContext.Configuration.DependencyResolver != null)
                actionContext.ActionArguments[Descriptor.ParameterName] =
                    actionContext.ControllerContext.Configuration.DependencyResolver.GetService(Descriptor.ParameterType);
            return Task.FromResult(0);
        }
    }
}