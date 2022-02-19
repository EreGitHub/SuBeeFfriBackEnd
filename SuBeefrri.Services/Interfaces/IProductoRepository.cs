using SuBeefrri.Core.Dtos;

namespace SuBeefrri.Services.Interfaces
{
    public interface IProductoRepository
    {
        Task<ProductoDTO> Add(ProductoDTO dto);
        Task GuardarFoto(int idProducto, string direccionFoto);
        Task<IEnumerable<ProductoListaDTO>> All();
        Task<IEnumerable<ProductoDTO>> Buscar(string query);
        Task Delete(int id);
        Task Update(int id, ProductoDTO dto);
    }
}