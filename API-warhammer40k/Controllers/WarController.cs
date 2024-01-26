using API_warhammer40k.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_warhammer40k.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarController : ControllerBase
    {
        public static List<WarCharacter>? WarCharacters { get; set; } =
            new List<WarCharacter> {
                new WarCharacter()
                {
                    Id = 0,
                    Name = "Archon",
                    Race = "Aeldari",
                    Image = "https://i.pinimg.com/474x/6d/3d/62/6d3d6227762452ae2e4a148405ca2803.jpg"
                },
                new WarCharacter()
                {
                    Id = 1,
                    Name = "Avatar of Khaine",
                    Race = "Aeldari",
                    Image = "https://qph.cf2.quoracdn.net/main-qimg-5fd71b3063f525b3b3bd0aa005f6462d-lq"
                },
                new WarCharacter()
                {
                    Id = 2,
                    Name = "Exalted Bloodletter",
                    Race = "Chaos",
                    Image = "https://www.belloflostsouls.net/wp-content/uploads/2017/03/bloodletter-horz.jpg"
                },
                new WarCharacter()
                {
                    Id = 3,
                    Name = "Exalted Champion",
                    Race = "Chaos",
                    Image = "https://www.belloflostsouls.net/wp-content/uploads/2015/12/chosen-csm.jpg"
                },
                new WarCharacter()
                {
                    Id = 4,
                    Name = "Chief Librarian",
                    Race = "Imperium",
                    Image = "https://artwork.40k.gallery/wp-content/uploads/2021/12/zhang-han-chief-librarian-tigurius.jpg"
                },
                new WarCharacter()
                {
                    Id = 5,
                    Name = "Deathwing Terminator",
                    Race = "Imperium",
                    Image = "https://64.media.tumblr.com/938109b1d356e41831e77847f4cb66b4/773a67e192b08bf4-f2/s1280x1920/cca68ad400bf39c2f81bf3e2b3c8e018e4e21978.jpg"
                },
                new WarCharacter()
                {
                    Id = 6,
                    Name = "Synapse",
                    Race = "Tyranids",
                    Image = "https://www.belloflostsouls.net/wp-content/uploads/2014/05/tyranid_zoanthrope_artwork_by_zergwing-d780178.jpg"
                },
                new WarCharacter()
                {
                    Id = 7,
                    Name = "Hive Tyrant",
                    Race = "Tyranids",
                    Image = "https://64.media.tumblr.com/bd0ebb9a6e941113c1f15e710da2c412/tumblr_inline_os2c05tT2h1ua1kv2_1280.jpg"
                },
                new WarCharacter()
                {
                    Id = 8,
                    Name = "Daemon Prince",
                    Race = "Chaos",
                    Image = "https://spikeybits.com/wp-content/uploads/2017/03/Demon-Prince.jpg"
                },
                new WarCharacter()
                {
                    Id = 9,
                    Name = "Harlequin",
                    Race = "Aeldari",
                    Image = "https://warhammerart.com/wp-content/uploads/2018/06/Harlequins.jpg"
                },
            };
        [HttpGet]
        public List<WarCharacter>? Get()
        {
            return WarCharacters;
        }

        [HttpGet("{id}")]
        public ActionResult<WarCharacter> Get(int id)
        {
            WarCharacter? characterToReturn = WarCharacters?.FirstOrDefault(c => c.Id == id);

            if (characterToReturn == null)
            {
                return NotFound("That ID does not exist!");
            }
            else
            {
                return Ok(characterToReturn);
            }
        }

        [HttpGet("search")]
        public ActionResult<List<WarCharacter>> Search([FromQuery] string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return BadRequest("Search query is required.");
            }

            List<WarCharacter>? searchResults = WarCharacters?
                .Where(character => character.Name.ToLower().Contains(q.ToLower()) || character.Race.ToLower().Contains(q.ToLower()))
                .ToList();

            if (searchResults == null || searchResults.Count == 0)
            {
                return NotFound("No results found.");
            }

            return Ok(searchResults);
        }
        [HttpPost]
        public ActionResult Post(WarCharacter character)
        {
            int nextId = WarCharacters?.Max(c => c.Id) + 1 ?? 0;

            WarCharacter newCharacter = new WarCharacter
            {
                Id = nextId,
                Name = character.Name,
                Race = character.Race
            };

            WarCharacters?.Add(newCharacter);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, WarCharacter updatedCharacter)
        {
            WarCharacter? existingCharacter = WarCharacters?.FirstOrDefault(c => c.Id == id);

            if (existingCharacter != null)
            {
                existingCharacter.Name = updatedCharacter.Name;
                existingCharacter.Race = updatedCharacter.Race;
                return Ok();
            }
            else
            {
                return NotFound("That ID does not exist!");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            WarCharacter? characterToDelete = WarCharacters?.FirstOrDefault(c => c.Id == id);

            if (characterToDelete != null)
            {
                WarCharacters?.Remove(characterToDelete);
                return Ok();
            }
            else
            {
                return NotFound("That ID does not exist!");
            }
        }

    }
}
