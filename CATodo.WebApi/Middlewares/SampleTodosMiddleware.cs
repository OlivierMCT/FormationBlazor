using CATodo.DAL;
using CATodo.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CATodo.WebApi.Middlewares {
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SampleTodosMiddleware {
        private readonly RequestDelegate _next;

        public SampleTodosMiddleware(RequestDelegate next) {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, CATodoContext dbContext) {
            if (dbContext.Database.EnsureCreated()) {
                CreateSampleTodos(dbContext);
            }
            return _next(httpContext);
        }

        private void CreateSampleTodos(CATodoContext context) {
            Random rd = new Random();
            List<string> names = new List<string>() {
                    "Travail", "Personnel", "Administratif", "Loisirs", "Idées", "Apéro", "Finances"
                };
            var categories = names
                .Select(n => new CategoryEntity() {
                    Name = n,
                    Color = $"#{rd.Next(0, 256):x2}{rd.Next(0, 256):x2}{rd.Next(0, 256):x2}{rd.Next(0, 256):x2}"
                })
                .ToList();
            context.AddRange(categories);
            context.SaveChanges();


            List<string> titles = new List<string>() {
                    "Corriger le bug #43587",
                    "Supprimer le thême 'Blink'",
                    "Ajouter un bouton 'Like'",
                    "Demander une augmentation",
                    "Poser des congés",
                    "Se préparer à l'entretien annuel",
                    "S'inscrire à la formation Blazor",
                    "Corriger le bug #98256",
                    "Installer Visual Studio 2022",
                    "Ajouter les Tests Unitaires",
                    "Supprimer tous les icones du bureau",
                    "Configurer GIT",
                    "Monter en compétences sur .net core",
                    "Installer Bootstrap 4",
                    "Apporter les chocolatines",
                    "Appeler le Helpdesk",
                    "Finir la documentation",
                    "Corriger le bug #12856",
                    "Installer un antivirus",
                    "Se désincrire des newsletter",
                    "Corriger le bug #22384",
                    "Reinstaller Windows 10",
                    "Préparer les vacances",
                    "Changer le fond d'écran",
                    "Améliorer les performances de l'app iPhone",
                    "Corriger le bug #56812",
                    "Regarder MongoDB",
                    "Visualiser la dernière conférence NG",
                    "Corriger le bug #45098",
                    "Changer la photo de profil",
                    "Monter le Kilimandjaro",
                    "Marcher sur la Muraille de Chine",
                    "Nager avec un requin en liberté",
                    "Faire un jogging dans Central Park",
                    "Poser les pieds sur un des pôles du globe",
                    "Manger du cochon d’inde au Pérou",
                    "Faire le tour de la Patagonie",
                    "Aller voir un match de foot au Brésil",
                    "Aller à Ushuaïa la ville la plus australe du monde",
                    "Voir les statues de l’île de Pâques",
                    "Nager à Palawan aux Philippines",
                    "Parcourir la forêt amazonienne en pirogue",
                    "Voir les chutes du Niagara",
                    "Voir un dragon de Komodo",
                    "Voir une éclipse",
                    "Voir le Grand Canyon",
                    "Apprendre le yoga",
                    "Tamponner sur toutes les pages de mon passeport",
                    "Descendre la route de la mort en VTT en Bolivie",
                    "Participer au carnaval de Rio",
                    "Voir une aurore boréale",
                    "Faire une balade en montgolfière",
                    "Sauter à l’élastique",
                    "Faire de la plongée sous-marine",
                    "Boire du sang de serpent",
                    "Faire du rafting",
                    "Voir des geisers",
                    "Marcher dans Sequoia National Park en Californie",
                    "Vivre plusieurs mois dans un pays étranger",
                    "Voir une baleine",
                    "Prendre le transsibérien",
                    "Faire l’amour sur une plage",
                    "Parcourir les route 66 aux États-Unis",
                    "Monter en haut de l’Empire State Building",
                    "Faire un saut en parachute",
                    "Faire un road trip en van en Australie",
                    "Faire du kitesurf",
                    "Parcourir les rizières à vélo",
                    "Passer une nuit près du cercle polaire",
                    "Écrire un livre",
                    "Voir les pyramides en Égypte",
                    "Apprendre le tango à Buenos Aires",
                    "Apprendre la salsa",
                    "Gravir le Mont Fuji",
                    "Snorkeler sur la Grande Barrière de Corail",
                    "Faire l’ascension d’un volcan",
                    "Voir le Taj Mahal",
                    "Naviguer sur une rivière souterraine",
                    "Faire la route des vins en Argentine",
                    "Discuter avec un moine tibétain",
                    "Faire un safari",
                    "Voir le rocher d’Uluru en Australie",
                    "Faire le trek de l’Inca jusqu’au Machu Picchu",
                    "Embrasser quelqu’un sous la pluie",
                    "Jouer comme figurant dans un film de Bollywood",
                    "Prendre un bain de minuit",
                    "Gravir une pyramide Aztèque ou Maya",
                    "Faire du wwoofing",
                    "Dormir à la belle étoile dans le désert",
                    "Faire du surf",
                    "Faire cracher un lama",
                    "Faire trek de plusieurs semaines au Népal",
                    "Aller sur les îles Galapagos",
                    "Faire de l’auto-stop",
                    "Dormir dans une maison sur pilotis",
                    "Voir les chutes d’Iguazu",
                    "Voir des kangourous",
                    "Rester éveillé toute la nuit",
                    "Traverser un pont de singe",
                    "Créer mon blog",
                    "Marcher dans le Sahara",
                    "Apprendre à jouer d’un instrument",
                    "Traverser un océan en voilier",
                    "Traverser l’équateur",
                    "Parcourir une longue distance en autostop",
                    "Faire la full moon party à KohPanghan en Thaïlande",
                    "Apprendre une nouvelle langue étrangère",
                    "Voir Pétra en Jordanie",
                    "Tomber amoureux(se)",
                    "Faire une retraite bouddhiste",
                    "Faire un voyage seul",
                    "Se baigner sous une cascade",
                    "Faire du chien de traîneau",
                    "Ne pas voir l’hiver pendant un an",
                    "Jouer au poker à Las Vegas",
                    "Voir un match de cricket en Inde",
                    "Grimper jusqu’au Christ Rédempteur à Rio",
                    "Manger un insecte",
                    "Prendre le tram à San Francisco",
                    "Voir les temples d’Angkor Wat au Cambodge",
                    "Voir de la lave en fusion sur un volcan",
                    "Faire du parapente",
                    "Avoir ma photo dans un journal étranger",
                    "Faire un câlin à un koala",
                    "Voir un match de boxe thaï",
                    "Nager avec des tortues",
                    "Me baigner dans un piscine d’eau chaude naturelle",
                    "Voir le Salar d’Uyuni en Bolivie",
                    "Naviguer sur la baie d’Halong au Vietnam"
                };
            List<string> descriptions = new List<string>() {
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum sed quam suscipit, venenatis lectus id, congue quam.", "Nunc commodo convallis leo tincidunt facilisis.", "Integer hendrerit aliquet magna nec malesuada.", "Ut sodales pulvinar risus nec pulvinar.", "Morbi in erat elementum, interdum magna vel, condimentum nisi.", "Cras vitae nisl id massa tristique ultricies.", "Praesent laoreet erat et nisi consectetur facilisis.", "Fusce pretium, urna non porttitor dignissim, augue lacus facilisis lectus, sed efficitur sapien lectus vel dui.", "Pellentesque ultricies egestas egestas.", "Maecenas finibus mattis dolor at cursus.", "Donec quis sem cursus, lacinia neque vel, aliquam metus.", "In et erat ac libero cursus ultrices vel at leo.", "Morbi est tortor, interdum eget scelerisque et, efficitur vulputate enim.", "Suspendisse vehicula porta blandit. Vivamus maximus et augue eu eleifend.", "Aliquam volutpat eros id tincidunt porta.", "Aenean at tempus ex, eget mollis nunc.", "Donec pulvinar nunc felis, eu rhoncus purus iaculis in.", "Sed mollis est est, eget tincidunt ipsum aliquam id.", "In sit amet lacinia erat, nec fermentum urna.", "Donec eu imperdiet odio.", "Suspendisse sit amet lacus vel sem hendrerit vehicula sed non ante.", "Maecenas a neque a ligula dignissim pretium.", "Morbi maximus porttitor nisi, vel ullamcorper leo aliquet sit amet.", "Donec auctor, mauris at scelerisque pellentesque, quam est convallis metus, nec pellentesque sem leo vitae lacus.", "Praesent aliquet quam at dui dictum, in vulputate tortor dignissim. Vestibulum varius bibendum nisl a malesuada.", "Morbi ac purus consequat, volutpat ante quis, consequat metus.", "Donec justo risus, aliquet et auctor in, maximus eget justo.", "Praesent vel felis porttitor orci tempor egestas in nec massa.", "Sed dignissim velit vitae tortor sodales interdum.", "Donec tincidunt lorem imperdiet tempor feugiat.", "Fusce at dui in ipsum scelerisque pulvinar.", "Pellentesque ex tellus, congue vel ex vehicula, ullamcorper hendrerit odio.", "In eu mauris aliquet, placerat ipsum nec, porta lorem.", "In vitae lectus augue.", "Duis malesuada id diam eu auctor.", "Sed laoreet tortor ut ultricies porttitor.", "In nec placerat mauris, ultrices accumsan lorem.", "Praesent ornare massa ut lobortis tristique.", "Nulla sit amet iaculis augue.", "Nunc vel eleifend erat.", "Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Vestibulum tempus sem eget ex egestas ultricies.", "Nulla vitae fringilla felis.", "Integer porta ut est eu tempor.", "In ultrices, lacus dapibus malesuada vehicula, est urna molestie neque, ut posuere mauris leo ut arcu.", "Praesent efficitur diam id metus dapibus egestas.", "Praesent ultricies congue congue.", "Fusce eget tempor justo, vel tincidunt purus.", "Nam sit amet commodo velit.", "Nunc consequat id augue quis pulvinar.", "Phasellus quis mi sed orci scelerisque aliquam.", "Maecenas fermentum dui nec risus consectetur egestas.", "In at faucibus turpis.", "Morbi porttitor urna nunc, id vehicula purus mollis ut.", "Nulla at est mi.", "Ut ac turpis non velit lobortis dapibus.", "Aenean porta, lacus ut egestas convallis, tortor mauris imperdiet elit, a aliquet nisl enim a sapien. Vestibulum ut dapibus turpis.", "Integer quis urna nec metus viverra ullamcorper.", "Suspendisse sagittis efficitur neque, at imperdiet nisi laoreet a.", "Integer eget ante blandit, consequat odio eget, aliquam urna.", "Morbi euismod vel felis sed scelerisque.", "Mauris fermentum, erat sit"
                };
            var todos = titles
                .Select(t => new TodoEntity() {
                    Category = categories.ElementAt(rd.Next(0, categories.Count)),
                    Description = descriptions.ElementAt(rd.Next(0, descriptions.Count)),
                    DueDate = DateTime.Today.AddDays(rd.Next(-60, 80)),
                    IsDone = rd.Next(0, 100) < 30,
                    Title = t,
                    Latitude = rd.Next(0, 100) < 60 ? rd.NextDouble() * rd.Next(-90, 90) : null
                })
                .ToList();
            todos.Where(t => t.Latitude.HasValue).ToList().ForEach(t => t.Longitude = rd.NextDouble() * rd.Next(-180, 180));
            context.AddRange(todos);
            context.SaveChanges();
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SampleTodosMiddlewareExtensions {
        public static IApplicationBuilder UseSampleTodosMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<SampleTodosMiddleware>();
        }
    }
}
