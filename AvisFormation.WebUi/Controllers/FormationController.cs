using AvisFormation.Data;
using AvisFormation.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvisFormation.WebUi.Controllers
{
    public class FormationController : Controller
    {
        // GET: Formation
        public ActionResult ToutesLesFormations()
        {

            List<Formation> listFormations = new List<Formation>();

            using(var context = new DbEntities())
            {
                 listFormations = context.Formation.ToList();
            }

            return View(listFormations);
        }

        public ActionResult DetailsFormation(string nomSeo)
        {
            var vm = new FormationAvecAvisViewModel();

            using(var context = new DbEntities())
            {
                var formationEntity = context.Formation.Where(f => f.NomSeo == nomSeo).FirstOrDefault();

                if(formationEntity == null)
                  return RedirectToAction("Accueil", "Home");

                vm.FormationNom = formationEntity.Nom;
                vm.FormationDescription = formationEntity.Description;
                vm.FormationNomSeo = formationEntity.NomSeo;
                vm.FormationUrl = formationEntity.Url;
                vm.Note = formationEntity.Avis.Average(a => a.Note);
                vm.NombreAvis = formationEntity.Avis.Count;
                vm.Avis = formationEntity.Avis.ToList();
            }

            return View(vm);

        }

        public void InsertDataToDatabase()
        {

            var newFormation = new Formation()
            {
                Nom = "Epe",
                Description = "Test"
            };

            using(var context = new DbEntities())
            {

                context.Formation.Add(newFormation);
                context.SaveChanges();

            }

        }


        public void UpdateDataToDatabase()
        {

            using(var context = new DbEntities())
            {
                var entityFormation = context.Formation.FirstOrDefault(f => f.NomSeo == "dd");

                if(entityFormation != null)
                {
                    entityFormation.Nom = "toto";
                }
                context.SaveChanges();
            }

        }

    }
}