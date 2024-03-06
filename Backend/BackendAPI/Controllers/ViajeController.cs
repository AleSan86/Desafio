using AutoMapper;
using BackendAPI.Dto;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Servicio.Contracts;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ViajeController> _logger;
        private readonly IViajeService _viajeService;

        public ViajeController(IMapper mapper, ILogger<ViajeController> logger, IViajeService viajeService)
        {
            _mapper = mapper;
            _logger = logger;
            _viajeService = viajeService;
        }

        [HttpGet("GetAllViajes")]
        public List<ViajeDto> GetAllViajes()
        {
            try
            {
                return _mapper.Map<List<ViajeDto>>(_viajeService.GetAllViajes());
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error intentando obtener todos los viajes.");
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetViajeById(int id)
        {
            if (id != 0)
            {
                //Verifico que el registro exista en la base de datos
                var resultadoConsulta = await _viajeService.GetById(id);

                if (resultadoConsulta != null)
                {
                    try
                    {
                        return Ok(resultadoConsulta);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Error intentando obtener el viaje " + id + ".");
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
                throw new Exception("No existe un viaje con el id informado.");
            }
            throw new Exception("Debe informar un id de (viaje) válido.");
        }

        [HttpPost("NuevoViaje")]
        public async Task<IActionResult> CrearViaje([FromBody] ViajeAMDto viaje)
        {
            if (viaje != null)
            {
                //Todo Validar con ModelState

                //Valido que no existe un viaje previo en esa fecha para el vehiculo indicado
                var resultado = _viajeService.GetAll().ToList();
                var consulta = resultado.Find(v => v.IdVehiculo == viaje.IdVehiculo
                               && v.Fecha.ToShortDateString() == viaje.Fecha.ToShortDateString());

                if (consulta == null)
                {
                    try
                    {
                        _viajeService.Save(_mapper.Map<Viaje>(viaje));
                        return await Task.FromResult<IActionResult>(Ok("El registro se guardó correctamente."));
                    }

                    catch (Exception ex)
                    {
                        _logger.LogInformation("Error intentando guardar el viaje.");
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
                return BadRequest($"Ya existe un viaje para el vehículo solicitado, en fecha: " + viaje.Fecha + ".");
            }
            throw new Exception("Debe informar datos válidos.");
        }

        [HttpPut("EditarViaje")]
        public async Task<IActionResult> EditarViaje(ViajeAMDto viaje)
        {
            if (viaje?.Id != 0 && viaje?.Id != null)
            {
                var resultadoConsulta = await _viajeService.GetById((int)viaje.Id);

                if (resultadoConsulta != null)
                {
                    try
                    {
                        resultadoConsulta.Activo = viaje.Activo;
                        resultadoConsulta.IdVehiculo = viaje.IdVehiculo;
                        resultadoConsulta.IdCiudad = viaje.IdCiudad;
                        resultadoConsulta.Fecha = viaje.Fecha;
                        resultadoConsulta.FechaAnterior = viaje.FechaAnterior;

                        _viajeService.Update(_mapper.Map<Viaje>(resultadoConsulta));
                        return await Task.FromResult<IActionResult>(Ok("Registro editado con éxito."));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Error intentando editar la ciudad de " + viaje + ".");
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
                throw new Exception("La ciudad indicada no existe en la base de datos.");
            }
            throw new Exception("La ciudad indicada no existe en la base de datos.");
        }

        [HttpPut("EliminarViaje/{id}")]
        public async Task<IActionResult> BorrarViaje(int id)
        {
            if (id != 0)
            {
                //Verifico que el registro exista en la base de datos
                var resultadoConsulta = await _viajeService.GetById(id);

                if (resultadoConsulta != null)
                {
                    try
                    {
                        resultadoConsulta.Activo = 0;
                        _viajeService.Update(resultadoConsulta);
                        return await Task.FromResult<IActionResult>(Ok($"El viaje del vehículo " + resultadoConsulta.Vehiculo +
                            " con destino a: " + resultadoConsulta.Ciudad + " fué eliminado con éxito."));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Error intentando eliminar el viaje " + resultadoConsulta.Id + ".");
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
                throw new Exception("El viaje ingresado ya fué eliminado o no existe.");
            }
            throw new Exception("Debe informar un id de (viaje) válido.");
        }

    }
}
