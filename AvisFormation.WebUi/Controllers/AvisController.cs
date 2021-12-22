using AvisFormation.Data;
using AvisFormation.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvisFormation.WebUi.Controllers
{
    public class AvisController : Controller
    {
        // GET: Avis
        public ActionResult LaisserUnAvis(string nomSeo)
        {
            var vm = new LaisserUnAvisViewModel();
            vm.NomSeo = nomSeo;

            using (var context = new DbEntities())
            {
                var formationEntity = context.Formation.FirstOrDefault(f => f.NomSeo == nomSeo);

                if (formationEntity == null)
                    return RedirectToAction("Accueil", "Home");

                vm.FormationName = formationEntity.Nom;
            }

                return View(vm);
        }

        public ActionResult SaveComment(string commentaire, string nom, string note, string nomSeo)
        {

            Avis nouvelAvis = new Avis()
            {
                DateAvis = DateTime.Now,
                Description = commentaire,
                //IdFormation = ,
                Nom = nom
            };

            double bNote = 0;
            if(!double.TryParse(note,NumberStyles.Any,CultureInfo.InvariantCulture, out bNote))
            {
                throw new Exception("Impossible de parser la note" + note);
            }

            nouvelAvis.Note = bNote;

            using(var context = new DbEntities())
            {
                var formationEntity = context.Formation.FirstOrDefault(f => f.NomSeo == nomSeo);

                if (formationEntity == null)
                    return RedirectToAction("Accueil", "Home");

                nouvelAvis.IdFormation = formationEntity.Id;

                context.Avis.Add(nouvelAvis);
                context.SaveChanges();
            }

            return View();
        }

    }
}