using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleFleet.Domain.Vehicles;
using VehicleFleet.Infrastructure.Data;
using VehicleFleet.Infrastructure.Data.EfCore;

namespace VehicleFleet.Application.Vehicles
{
    public class VehicleService : IVehicleService
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleReporitory;
        private readonly IBrandRepository _brandReporitory;
        private readonly IModelRepository _modelReporitory;
        private readonly ILogger<VehicleService> _logger;
        private readonly VehicleFleetContext _context;

        public VehicleService(IMapper mapper, IVehicleRepository vehicleReporitory, IBrandRepository brandReporitory, IModelRepository modelReporitory, ILogger<VehicleService> logger, VehicleFleetContext context)
        {
            _mapper = mapper;
            _vehicleReporitory = vehicleReporitory;
            _brandReporitory = brandReporitory;
            _modelReporitory = modelReporitory;
            _logger = logger;
            _context = context;
        }

        public VehicleDto Add(VehicleDto vehicle)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Brand brand = _brandReporitory.GetById(vehicle.BrandId);
                    if (brand == null)
                    {
                        brand = Brand.Create(vehicle.BrandName);
                        if (!_brandReporitory.Add(brand))
                        {
                            throw new Exception("Marka eklenemedi!");
                        }
                    }

                    Model model = _modelReporitory.GetById(vehicle.ModelId);
                    if (model == null)
                    {
                        model = Model.Create(brand, vehicle.ModelName);
                        if (!_modelReporitory.Add(model))
                        {
                            throw new Exception("Model Eklenemedi!");
                        }
                    }

                    Vehicle _vehicle = Vehicle.Create(vehicle.PlateNumber, brand, model, vehicle.Age);

                    if (!_vehicleReporitory.Add(_vehicle))
                    {
                        throw new Exception("Arac Eklenemedi!");
                    }

                    transaction.Commit();
                    _logger.LogInformation("added", vehicle);

                    return _mapper.Map<VehicleDto>(vehicle);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex, "error");
                    return null;
                }
            }
        }

        public bool Delete(Guid id)
        {
            if (!_vehicleReporitory.Delete(id))
            {
                _logger.LogError("error", id);
                throw new Exception("Arac silinemedi");
            }

            _logger.LogInformation("Arac silindi", id);
            return true;
        }

        public List<BrandDto> GetBrands()
        {
            List<Brand> brands = _brandReporitory.GetAll().OrderBy(x => x.Created).ToList();

            return _mapper.Map<List<BrandDto>>(brands);
        }

        public VehicleDto GetById(Guid id)
        {
            Vehicle vehicle = _vehicleReporitory.GetAll().OrderBy(x => x.Created).FirstOrDefault(x => x.Id == id);

            VehicleDto vehicleDto = _mapper.Map<VehicleDto>(vehicle);
            return vehicleDto;
        }

        public List<ModelDto> GetModels()
        {
            List<Model> models = _modelReporitory.GetAll().OrderBy(x => x.Created).ToList();

            return _mapper.Map<List<ModelDto>>(models);
        }

        public List<VehicleDto> GetVehicles()
        {
            List<Vehicle> vehicles = _vehicleReporitory.GetAll().OrderByDescending(x => x.Created).ToList();

            List<VehicleDto> vehicleDtos = _mapper.Map<List<VehicleDto>>(vehicles);
            return vehicleDtos;
        }

        public bool Update(VehicleDto vehicle)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Brand brand = _brandReporitory.GetById(vehicle.BrandId);
                    Model model = _modelReporitory.GetById(vehicle.ModelId);

                    if (model.BrandId != brand.Id)
                    {
                        throw new Exception("Model markaya ait degil");
                    }

                    Vehicle _vehicle = _vehicleReporitory.GetById(vehicle.Id);

                    _vehicle.Age = vehicle.Age;
                    _vehicle.PlateNumber = vehicle.PlateNumber;
                    _vehicle.ModelId = model.Id;
                    _vehicle.BrandId = brand.Id;

                    if (!_vehicleReporitory.Update(_vehicle))
                    {
                        throw new Exception("Arac guncellenemedi");
                    }

                    transaction.Commit();
                    _logger.LogInformation("added", vehicle);

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex, "error");
                    return false;
                }
            }
        }
    }
}