#nullable enable

namespace WS.Models.Revit2023
{
    using Autodesk.Revit.DB;
    using Contracts;
    using System;
    using WS.Models.Revit2020;

    public class GetParameterValueService : IGetPropertyValue
    {
        private GetElementByIdService _getElementByIdService;

        public GetParameterValueService(
            GetElementByIdService getElementByIdService)
        {
            _getElementByIdService = getElementByIdService;
        }

        public string GetPropertyValue(
            string entityId,
            string propertyName,
            object? units)
        {
            var element = _getElementByIdService
                .GetElement(entityId);

            var parameter = element
                .LookupParameter(propertyName);

            string res;

            switch (parameter.StorageType)
            {
                case StorageType.None:
                    throw new ArgumentException();
                case StorageType.Integer:
                    res = parameter.AsInteger().ToString();
                    break;
                case StorageType.Double:
                    res = GetFromDouble(parameter, units);
                    break;
                case StorageType.String:
                    res = parameter.AsString();
                    break;
                case StorageType.ElementId:
                    res = parameter.AsElementId().ToString();
                    break;
                default:
                    throw new ArgumentException(
                        parameter.StorageType.ToString());
            }

            return res;
        }

        private string GetFromDouble(
            Parameter parameter,
            object? units)
        {
            var value = parameter.AsDouble();

            var res = value.ToString();

            if (units is ForgeTypeId ft)
            {
                res = UnitUtils
                    .ConvertFromInternalUnits(
                        value,
                        ft)
                    .ToString();
            }

            return res;
        }
    }
}