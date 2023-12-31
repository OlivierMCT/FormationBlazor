using CATodo.DAL;
using CATodo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CATodo.Tests {
    [TestClass]
    public class CATodoContextUnitTest {
        [TestMethod]
        public void CreateDatabaseSchema() {
            DbContextOptionsBuilder builder = new();
            builder.UseSqlServer(@"server=(localdb)\MsSqlLocalDb;Database=TodoTestCreation;Trusted_Connection=True;");
            builder.LogTo(Console.WriteLine);

            DbContextOptions options = builder.Options;

            CATodoContext context = new CATodoContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [TestMethod]
        public void CreateCategoryWithGuid() {
            DbContextOptionsBuilder builder = new();
            builder.UseSqlServer(@"server=(localdb)\MsSqlLocalDb;Database=CreateCategoryWithGuid;Trusted_Connection=True;");
            builder.LogTo(Console.WriteLine);

            DbContextOptions options = builder.Options;

            CATodoContext context = new CATodoContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var cat = new CategoryEntity() { Color = "azerty", Name = "Test" };
            context.Add(cat);
            context.SaveChanges();

            context.Entry(cat).Reload();
            Assert.IsTrue(cat.LastUpdated != new DateTime());
            Assert.IsTrue(cat.RowId != Guid.Empty);
        }

        [TestMethod]
        public void CreateSampleTodos() {
            DbContextOptionsBuilder builder = new();
            builder.UseSqlServer(@"server=(localdb)\MsSqlLocalDb;Database=CreateSampleTodos;Trusted_Connection=True;");
            builder.LogTo(Console.WriteLine);

            DbContextOptions options = builder.Options;

            CATodoContext context = new CATodoContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            CreateSampleTodos(context);
        }

        private void CreateSampleTodos(CATodoContext context) {
            DbContextOptionsBuilder builder = new();
            builder.UseSqlServer(@"server=(localdb)\MsSqlLocalDb;Database=CreateCategoryWithGuid;Trusted_Connection=True;");
            builder.LogTo(Console.WriteLine);

            DbContextOptions options = builder.Options;

            Random rd = new Random();
            List<string> names = new List<string>() {
                    "Travail", "Personnel", "Administratif", "Loisirs", "Id�es", "Ap�ro", "Finances"
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
                    "Supprimer le th�me 'Blink'",
                    "Ajouter un bouton 'Like'",
                    "Demander une augmentation",
                    "Poser des cong�s",
                    "Se pr�parer � l'entretien annuel",
                    "S'inscrire � la formation Blazor",
                    "Corriger le bug #98256",
                    "Installer Visual Studio 2022",
                    "Ajouter les Tests Unitaires",
                    "Supprimer tous les icones du bureau",
                    "Configurer GIT",
                    "Monter en comp�tences sur .net core",
                    "Installer Bootstrap 4",
                    "Apporter les chocolatines",
                    "Appeler le Helpdesk",
                    "Finir la documentation",
                    "Corriger le bug #12856",
                    "Installer un antivirus",
                    "Se d�sincrire des newsletter",
                    "Corriger le bug #22384",
                    "Reinstaller Windows 10",
                    "Pr�parer les vacances",
                    "Changer le fond d'�cran",
                    "Am�liorer les performances de l'app iPhone",
                    "Corriger le bug #56812",
                    "Regarder MongoDB",
                    "Visualiser la derni�re conf�rence NG",
                    "Corriger le bug #45098",
                    "Changer la photo de profil",
                    "Monter le Kilimandjaro",
                    "Marcher sur la Muraille de Chine",
                    "Nager avec un requin en libert�",
                    "Faire un jogging dans Central Park",
                    "Poser les pieds sur un des p�les du globe",
                    "Manger du cochon d�inde au P�rou",
                    "Faire le tour de la Patagonie",
                    "Aller voir un match de foot au Br�sil",
                    "Aller � Ushua�a la ville la plus australe du monde",
                    "Voir les statues de l��le de P�ques",
                    "Nager � Palawan aux Philippines",
                    "Parcourir la for�t amazonienne en pirogue",
                    "Voir les chutes du Niagara",
                    "Voir un dragon de Komodo",
                    "Voir une �clipse",
                    "Voir le Grand Canyon",
                    "Apprendre le yoga",
                    "Tamponner sur toutes les pages de mon passeport",
                    "Descendre la route de la mort en VTT en Bolivie",
                    "Participer au carnaval de Rio",
                    "Voir une aurore bor�ale",
                    "Faire une balade en montgolfi�re",
                    "Sauter � l��lastique",
                    "Faire de la plong�e sous-marine",
                    "Boire du sang de serpent",
                    "Faire du rafting",
                    "Voir des geisers",
                    "Marcher dans Sequoia National Park en Californie",
                    "Vivre plusieurs mois dans un pays �tranger",
                    "Voir une baleine",
                    "Prendre le transsib�rien",
                    "Faire l�amour sur une plage",
                    "Parcourir les route 66 aux �tats-Unis",
                    "Monter en haut de l�Empire State Building",
                    "Faire un saut en parachute",
                    "Faire un road trip en van en Australie",
                    "Faire du kitesurf",
                    "Parcourir les rizi�res � v�lo",
                    "Passer une nuit pr�s du cercle polaire",
                    "�crire un livre",
                    "Voir les pyramides en �gypte",
                    "Apprendre le tango � Buenos Aires",
                    "Apprendre la salsa",
                    "Gravir le Mont Fuji",
                    "Snorkeler sur la Grande Barri�re de Corail",
                    "Faire l�ascension d�un volcan",
                    "Voir le Taj Mahal",
                    "Naviguer sur une rivi�re souterraine",
                    "Faire la route des vins en Argentine",
                    "Discuter avec un moine tib�tain",
                    "Faire un safari",
                    "Voir le rocher d�Uluru en Australie",
                    "Faire le trek de l�Inca jusqu�au Machu Picchu",
                    "Embrasser quelqu�un sous la pluie",
                    "Jouer comme figurant dans un film de Bollywood",
                    "Prendre un bain de minuit",
                    "Gravir une pyramide Azt�que ou Maya",
                    "Faire du wwoofing",
                    "Dormir � la belle �toile dans le d�sert",
                    "Faire du surf",
                    "Faire cracher un lama",
                    "Faire trek de plusieurs semaines au N�pal",
                    "Aller sur les �les Galapagos",
                    "Faire de l�auto-stop",
                    "Dormir dans une maison sur pilotis",
                    "Voir les chutes d�Iguazu",
                    "Voir des kangourous",
                    "Rester �veill� toute la nuit",
                    "Traverser un pont de singe",
                    "Cr�er mon blog",
                    "Marcher dans le Sahara",
                    "Apprendre � jouer d�un instrument",
                    "Traverser un oc�an en voilier",
                    "Traverser l��quateur",
                    "Parcourir une longue distance en autostop",
                    "Faire la full moon party � KohPanghan en Tha�lande",
                    "Apprendre une nouvelle langue �trang�re",
                    "Voir P�tra en Jordanie",
                    "Tomber amoureux(se)",
                    "Faire une retraite bouddhiste",
                    "Faire un voyage seul",
                    "Se baigner sous une cascade",
                    "Faire du chien de tra�neau",
                    "Ne pas voir l�hiver pendant un an",
                    "Jouer au poker � Las Vegas",
                    "Voir un match de cricket en Inde",
                    "Grimper jusqu�au Christ R�dempteur � Rio",
                    "Manger un insecte",
                    "Prendre le tram � San Francisco",
                    "Voir les temples d�Angkor Wat au Cambodge",
                    "Voir de la lave en fusion sur un volcan",
                    "Faire du parapente",
                    "Avoir ma photo dans un journal �tranger",
                    "Faire un c�lin � un koala",
                    "Voir un match de boxe tha�",
                    "Nager avec des tortues",
                    "Me baigner dans un piscine d�eau chaude naturelle",
                    "Voir le Salar d�Uyuni en Bolivie",
                    "Naviguer sur la baie d�Halong au Vietnam"
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
                    Latitude = rd.Next(0,100) < 60 ? rd.NextDouble() * rd.Next(-90, 90) : null
                })
                .ToList();
            todos.Where(t => t.Latitude.HasValue).ToList().ForEach(t => t.Longitude = rd.NextDouble() * rd.Next(-180, 180));
            context.AddRange(todos);
            context.SaveChanges();
        }
    }
}
