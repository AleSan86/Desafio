using AutoMapper;
using BackendAPI.Dto;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Servicio.Contracts;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CiudadController> _logger;
        private readonly ICiudadService _ciudadService;

        public CiudadController(IMapper mapper, ILogger<CiudadController> logger, ICiudadService ciudadService)
        {
            _mapper = mapper;
            _logger = logger;
            _ciudadService = ciudadService;
        }

        [HttpGet("GetAllCiudades")]
        public List<CiudadDto> GetAllCiudades()
        {
            try
            {
                return _mapper.Map<List<CiudadDto>>(_ciudadService.GetAllCiudades());
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error intentando obtener ciudades desde la base de datos.");
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCiudadById(int id)
        {
            if (id != 0)
            {
                //Verifico que el registro exista en la base de datos
                var resultadoConsulta = await _ciudadService.GetById(id);

                if (resultadoConsulta != null)
                {
                    try
                    {
                        return Ok(resultadoConsulta);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Error intentando obtener el ciudad " + id + ".");
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
                throw new Exception("No existe una ciudad con el id informado.");
            }
            throw new Exception("Debe informar un id de (Ciudad) válido.");
        }

        [HttpPost("NuevaCiudad")]
        public async Task<IActionResult> CrearCiudad(CiudadCrearDto ciudad)
        {
            if (ciudad != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Revise los campos obligatorios.");
                }

                //Valido que no exista previamente
                //Todo Normalizar para validar
                var resultado = _ciudadService.GetAllCiudades().ToList();
                var consulta = resultado.Find(c => c.Descripcion == ciudad.Descripcion);

                if (consulta == null)
                {
                    try
                    {
                        _ciudadService.Save(_mapper.Map<Ciudad>(ciudad));

                        return await Task.FromResult<IActionResult>(Ok($"Se ha guardado la ciudad: " + ciudad.Descripcion + " en la base de datos."));
                    }

                    catch (Exception ex)
                    {
                        _logger.LogInformation("Error intentando crear una nueva ciudad.");
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
                return BadRequest($"Ya existe una ciudad con el nombre: " + ciudad.Descripcion + ".");
            }
            throw new Exception("Debe informar datos válidos.");
        }

        [HttpPut("EditarCiudad")]
        public async Task<IActionResult> EditarCiudad(CiudadDto ciudad)
        {
            if (ciudad != null)
            {
                var resultadoConsulta = await _ciudadService.GetById(ciudad.Id);

                if (resultadoConsulta != null)
                {
                    try
                    {
                        //Todo Normalizar para verificar
                        resultadoConsulta.Descripcion = ciudad.Descripcion;

                        _ciudadService.Update(_mapper.Map<Ciudad>(resultadoConsulta));
                        return await Task.FromResult<IActionResult>(Ok("Registro editado con éxito."));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Error intentando editar la ciudad de " + ciudad + ".");
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
                throw new Exception("La ciudad indicada no existe en la base de datos.");
            }
            throw new Exception("La ciudad indicada no existe en la base de datos.");
        }

        [HttpDelete("EliminarCiudad/{id}")]
        public async Task<IActionResult> BorrarCiudad(int id)
        {
            if (id != 0)
            {
                var resultadoConsulta = await _ciudadService.GetById(id);

                if (resultadoConsulta != null)
                {
                    try
                    {
                        resultadoConsulta.Activo = 0;
                        _ciudadService.Update(resultadoConsulta);
                        return await Task.FromResult<IActionResult>(Ok("Ciudad eliminada de la base de datos."));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Error intentando eliminar la ciudad " + resultadoConsulta.Descripcion + ".");
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
                throw new Exception("La ciudad ingresada ya fué eliminada o no existe.");
            }
            throw new Exception("Debe informar un id de (ciudad) válido.");
        }


    }
}
