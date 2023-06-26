using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villalist = new List<VillaDTO>() {
             new VillaDTO { Id=1, Name="Pool View",Sqft=100,Ocuupancy=4},
             new VillaDTO { Id=2,Name="Beach View",Sqft=200,Ocuupancy=6}
             };

    }
}
