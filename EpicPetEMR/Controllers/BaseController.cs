using EpicPetEMR.Api.Data;
using EpicPetEMR.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EpicPetEMR.Api.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected int GetUserId() =>
         int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // Returns a queryable of only pets belonging to the current user's families
    protected IQueryable<Pet> GetUserPets(AppDbContext db)
    {
        var userId = GetUserId();
        return db.Pets
            .Where(p => db.FamilyMemberships
                .Any(m => m.UserId == userId && m.FamilyId == p.FamilyId));
    }
}