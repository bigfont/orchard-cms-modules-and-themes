using Bootstrap_3_1_1.Carousel.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.MediaLibrary.Services;


namespace Bootstrap_3_1_1.Carousel.Drivers
{
    public class CarouselDriver : ContentPartDriver<CarouselPart>
    {
        private readonly IMediaLibraryService _mediaLibraryService;

        public CarouselDriver(IMediaLibraryService mediaLibrarySvc)
        {
            _mediaLibraryService = mediaLibrarySvc;
        }

        protected override DriverResult Display(CarouselPart part, string displayType, dynamic shapeHelper)
        {
            var path = "slideshow";
            var images = _mediaLibraryService
                .GetMediaContentItems(path, 0, int.MaxValue, null, null)
                .OrderBy(p => p.FileName)
                .ToList();

            return ContentShape("Parts_Carousel", () => shapeHelper.Parts_Carousel(
                ContentPart: part,
                Images: images,
                Path: path));
        }

        //GET
        protected override DriverResult Editor(CarouselPart part, dynamic shapeHelper)
        {

            return ContentShape("Parts_Carousel_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/Carousel",
                    Model: part,
                    Prefix: Prefix));
        }
        //POST
        protected override DriverResult Editor(CarouselPart part, IUpdateModel updater, dynamic shapeHelper)
        {

            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}