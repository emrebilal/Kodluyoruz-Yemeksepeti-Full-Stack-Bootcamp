using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week2_WebAPI.Models;

namespace Week2_WebAPI.Mapping
{
    public static class MappingExtension
    {
        public static List<CarDTO> ToViewModel(this List<Car> cars)
        {
            List<CarDTO> resultItems = new List<CarDTO>();
            foreach (var item in cars)
            {
                resultItems.Add(new CarDTO
                {
                    BrandName = item.BrandName,
                    ModelYear = item.ModelYear,
                    Color = item.Color,
                    DailyPrice = item.DailyPrice
                });
            }
            return resultItems;
        }
    }
}
