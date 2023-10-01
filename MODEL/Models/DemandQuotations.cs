using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class DemandQuotations
    {
        public Request RequestInfo { get; set; }
        public List<Quotation> Quotations { get; set; }
        public List<QuotationPart> QuotationParts { get; set; }
        public List<QuotationPartRef> QuotationPartRef { get; set; }
        public List<QuotationPartRef> RejectedSupplierParts { get; set; }
        public List<RequestedPart> RequestedParts { get; set; }
        public List<Image> QuotationPartsImages { get; set; }
        public List<Image> RequestedPartsImages { get; set; }
        public List<PartManufacturer> PartManufacturers { get; set; }
        public List<Country> Countries { get; set; }
        public List<ManufacturerRegion> ManufacturerRegions { get; set; }
        public List<City> Cities { get; set; }
        public List<Quotation> SupplierQuotations { get; set; }
        public List<Quotation> ReferredSupplierQuotations { get; set; }
        public List<Quotation> RejectedSupplierOffers { get; set; }
        public int TotalPendingParts { get; set; }

        public List<RequestTask> RequestTasks { get; set; }
        public List<AccidentMarker> AccidentMarkers { get; set; }
        public List<Note> Notes { get; set; }
        public List<Image> AccidentImages { get; set; }
        public List<Quotation> ReviewNotes { get; set; }
        public List<RequestedPart> RequestedPartsPrice { get; set; }
    }
}
