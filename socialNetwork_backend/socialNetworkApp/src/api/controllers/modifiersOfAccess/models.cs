using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using socialNetworkApp.api.controllers.users;
using socialNetworkApp.db;
using SuccincT.PatternMatchers;

namespace socialNetworkApp.api.controllers.modifiersOfAccess;

// [Table("modifiers_of_access", Schema = "public")]
// public class ModifiersOfAccessDb : AbstractEntity
// {
//     [Key] [Column("id")] public Guid Id { get; set; }
//     [Column("name")] [Unicode] public string Name { get; set; } = default!;
//
//     public List<UserDb> Users { get; set; }= new List<UserDb>();
//
//     public override async Task OnStart(BaseBdConnection db)
//     {
//         
//     }
// }