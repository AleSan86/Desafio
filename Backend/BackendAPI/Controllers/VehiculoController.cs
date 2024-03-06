using AutoMapper;
using BackendAPI.Dto;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Servicio.Contracts;
using Servicio.Implementation;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<VehiculoController> _logger;
        private readonly IVehiculoService _vehiculoService;
        private readonly ITipoVehiculoService _tipoVehiculoService;

        public VehiculoController(IMapper mapper, ILogger<VehiculoController> logger, IVehiculoService vehiculoService, ITipoVehiculoService tipoVehiculoService)
        {
            _mapper = mapper;
            _logger = logger;
            _vehiculoService = vehiculoService;
            _tipoVehiculoService = tipoVehiculoService;
        }

        [HttpGet("GetAllVehiculos")]
        public List<VehiculoDto> GetAllVehiculos()
        {
            try
            {
                return _mapper.Map<List<VehiculoDto>>(_vehiculoService.GetAllVehiculos());
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error intentando obtener vehículos.");
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehiculoById(int id)
        {
            if (id != 0)
            {
                //Verifico que el registro exista en la base de datos
                var resultadoConsulta = await _vehiculoService.GetById(id);

                if (resultadoConsulta != null)
                {
                    try
                    {
                        return Ok(resultadoConsulta);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Error intentando obtener el vehiculo " + id + ".");
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
                throw new Exception("No existe un vehiculo con el id informado.");
            }
            throw new Exception("Debe informar un id de (Vehiculo) válido.");
        }

        [HttpPost("NuevoVehiculo")]
        public async Task<IActionResult> CrearVehiculo([FromBody] VehiculoAltaDto vehiculo)
        {
            if (vehiculo != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Revise los campos obligatorios.");
                }

                //Valido que no exista previamente
                //Todo ¿Hace falta normalizar?
                var resultado = _vehiculoService.GetAll().ToList();
                var tipoVehiculo = _tipoVehiculoService.GetAll().ToList().Where(tv => tv.Id == vehiculo.IdTipoVehiculo);
                var consulta = resultado.Find(v => v.Patente == vehiculo.Patente);

                _mapper.Map<VehiculoAltaDto>(vehiculo);

                if (consulta == null)
                {
                    try
                    {
                        _vehiculoService.Save(_mapper.Map<Vehiculo>(vehiculo));

                        return await Task.FromResult<IActionResult>(Ok("Vehículo creado con éxito."));
                    }

                    catch (Exception ex)
                    {
                        _logger.LogInformation("Error intentando guardar un nuevo vehículo.");
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
            }
            throw new Exception("Debe informar un vehiculo válido.");
        }

        [HttpPut("EditarVehiculo")]
        public async Task<IActionResult> EditarVehiculo(VehiculoEditarDto vehiculo)
        {
            if (vehiculo?.Id != 0 && vehiculo?.Id != null)
            {
                var resultadoConsulta = await _vehiculoService.GetById((int)vehiculo.Id);

                if (resultadoConsulta != null)
                {
                    try
                    {
                        resultadoConsulta.Patente = vehiculo.Patente;
                        resultadoConsulta.Marca = vehiculo.Marca;
                        resultadoConsulta.Anio = vehiculo.Anio;
                        resultadoConsulta.Tara = vehiculo.Tara;

                        _vehiculoService.Update(_mapper.Map<Vehiculo>(resultadoConsulta));
                        return await Task.FromResult<IActionResult>(Ok("Vehículo editado con éxito."));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Error intentando editar un vehículo.");
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
                throw new Exception("El vehiculo indicado no existe en la base de datos.");
            }
            throw new Exception("El vehiculo indicado no existe en la base de datos.");
        }

        [HttpDelete("EliminarVehiculo/{id}")]
        public async Task<IActionResult> BorrarVehiculo(int id)
        {
            if (id != 0)
            {
                var resultadoConsulta = await _vehiculoService.GetById(id);

                if (resultadoConsulta != null)
                {
                    try
                    {
                        resultadoConsulta.Activo = 0;
                        _vehiculoService.Update(resultadoConsulta);
                        return await Task.FromResult<IActionResult>(Ok("Vehículo eliminado con éxito."));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Error intentando eliminar un vehículo.");
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
                throw new Exception("El vehiculo ingresado ya fué eliminado o no existe.");
            }
            throw new Exception("Debe informar un id (vehiculo) válido.");
        }

    }
}
