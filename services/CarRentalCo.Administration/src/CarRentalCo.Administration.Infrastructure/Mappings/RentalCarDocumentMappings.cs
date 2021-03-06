﻿using CarRentalCo.Administration.Application.RentalCars.Dtos;
using CarRentalCo.Administration.Domain.RentalCars;
using CarRentalCo.Administration.Infrastructure.Mongo.RentalCars;
using CarRentalCo.Common.Infrastructure.Mongo;

namespace CarRentalCo.Administration.Infrastructure.Mappings
{
    public static class RentalCarDocumentMappings
    {
        public static RentalCar ToAggregate(this RentalCarDocument doc)
            => new RentalCar(new RentalCarId(doc.Id), doc.Specification.ToValueObject(), doc.OperatingInfo.ToValueObject(),
                doc.VinNumber, doc.Description, doc.PricePerDay, doc.ImageUrl);

        public static RentalCarDto ToDto(this RentalCarDocument doc)
            => new RentalCarDto
            {
                Id = doc.Id,
                Description = doc.Description,
                ImageUrl = doc.ImageUrl,
                PricePerDay = doc.PricePerDay,
                VinNumber = doc.VinNumber,
                OperatingInfo = new RentalCarOperatingInfoDto
                {
                    InsurrenceValidThru = doc.OperatingInfo.InsurrenceValidThru,
                    OilValidThru = doc.OperatingInfo.OilValidThru,
                    TechnicalReviewValidThru = doc.OperatingInfo.TechnicalReviewValidThru
                },
                Specification = new RentalCarSpecificationDto
                {
                    Brand = doc.Specification.Brand,
                    Colour = (ColourDto)doc.Specification.Colour,
                    Model = doc.Specification.Model,
                    ProductionDate = doc.Specification.ProductionDate
                }
            };

        public static RentalCarSpecification ToValueObject(this RentalCarSpecificationDocument doc)
            => new RentalCarSpecification(doc.Brand, doc.Model, doc.ProductionDate, (Colour)doc.Colour);

        public static RentalCarOperatingInfo ToValueObject(this RentalCarOperatingInfoDocument doc)
            => new RentalCarOperatingInfo(doc.TechnicalReviewValidThru, doc.InsurrenceValidThru, doc.OilValidThru);

        public static RentalCarDocument ToDocument(this RentalCar rentalCar)
            => new RentalCarDocument
            { 
                Id = rentalCar.Id.Value,
                Description = rentalCar.Description, 
                PricePerDay = rentalCar.PricePerDay,
                VinNumber = rentalCar.VinNumber,
                ImageUrl = rentalCar.ImageUrl,
                OperatingInfo = new RentalCarOperatingInfoDocument
                {
                    InsurrenceValidThru = rentalCar.OperatingInfo.InsurrenceValidThru,
                    OilValidThru = rentalCar.OperatingInfo.OilValidThru,
                    TechnicalReviewValidThru = rentalCar.OperatingInfo.TechnicalReviewValidThru
                },
                Specification = new RentalCarSpecificationDocument
                {
                    Brand = rentalCar.Specification.Brand,
                    Colour = (ColourDocument)rentalCar.Specification.Colour,
                    Model = rentalCar.Specification.Model,
                    ProductionDate = rentalCar.Specification.ProductionDate
                }
            };



    }
}
