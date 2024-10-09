using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.Core.DTO.AuthViewModel.FilesModel
{
    public class PathsModel
    {
        [Required(ErrorMessage = "Should Enter Name Path"), DisplayName("Name Path"), StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Should Enter Description Path"), DisplayName("Description Path"), StringLength(250)]
        public string Description { get; set; }
    }
}
