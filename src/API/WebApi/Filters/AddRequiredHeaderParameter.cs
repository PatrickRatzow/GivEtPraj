using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Commentor.GivEtPraj.WebApi.Filters;

public class AddRequiredHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-DeviceId",
            In = ParameterLocation.Header,
            Description = "The users device id",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "String",
                Default = new OpenApiString("B4275AA5-31D0-40D9-8FD2-CBE7357BE21A")
            }
        });

    }
}