// -------------------------------------------------------------------------------------------------------------
//  DependencyResolverParameterBindingAttribute.cs created by DEP on 2017/01/13
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Backup.Web.API.IoC
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class DependencyResolverParameterBindingAttribute : ParameterBindingAttribute
    {
        /// <summary>
        /// Gets the parameter binding.
        /// </summary>
        /// <param name="parameter">The parameter description.</param>
        /// <returns>
        /// The parameter binding.
        /// </returns>
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            return new DependencyResolverParameterBinding(parameter);
        }
    }
}