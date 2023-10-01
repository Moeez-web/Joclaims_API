using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
   public class TechnicalNotes
    {
        public int TechnicalNoteID { get; set; }
        public string AccidentNo { get; set; }
        public string ReportSubject { get; set; }
        public string FromEmployee { get; set; }
        public string ContractNumber { get; set; }
        public double? DebitAccount { get; set; }
        public double? CreditBalance { get; set; }
        public string ToEmployee { get; set; }
        public double? InsuredAmount { get; set; }
        public bool? Repair { get; set; }
        public string PrevioutAccidentRecord { get; set; }
        public double? Premium { get; set; }
        public double? Compensation { get; set; }
        public string InsuredResult { get; set; }
        public int? AccidentResponsibility { get; set; }
        public string KrokiSketch { get; set; }
        public string PoliceReport { get; set; }
        public string Cause { get; set; }

        public string IVDriverFault { get; set; }
        public string TPLDriverFault { get; set; }
        public string ReverseCase { get; set; }
        public bool? PreviousAccident { get; set; }
        public string PreliminaryAgreementInsured { get; set; }
        public string DisagreementPoints { get; set; }
        public string ProposalSolution { get; set; }
        public string LossAdjusterName { get; set; }
        public string LossAdjusterReportResult { get; set; }
        public string TechnicalExplanation { get; set; }
        public string CompensationProposal { get; set; }
        public double? Recovery { get; set; }
        public double? PaidCompensation { get; set; }
        public double? FullPeriodPremium { get; set; }
        public double? LossAdjusterReport { get; set; }
        public double? CompensationCost { get; set; }
        public double? MarketValue { get; set; }
        public double? ApproxSalvageAmount { get; set; }
        public double? CompensationAmount { get; set; }
        public double? SalvageBestAmount { get; set; }
        public double? NetLoss { get; set; }
        public string InjuredName { get; set; }
        public int? InjuredAge { get; set; }
        public string InjuredLevel { get; set; }
        public string LegalOpenion { get; set; }
        public string MedicalOpenion { get; set; }
        public string MedicalExplanation { get; set; }
        public string TechnicalProcedure { get; set; }
        public string LogoURL { get; set; }
        public double? TreatmentExpense { get; set; }
        public double? DisabilityCompensation { get; set; }
        public double? FutureOperation { get; set; }
        public string FinalMedicalReportUrl { get; set; }
        public string PenaltyRulingDecisionUrl { get; set; }
        public string RegionalCommitteUrl { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromDate { get; set; }
        public int CompanyID { get; set; }
        public int ObjectTypeID { get; set; }
        public double TRTotalAmount { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? AssignLossAdjuster { get; set; }
        public string Note { get; set; }
        public string ComVehicleOwnerName { get; set; }
        public string ComPlateNo { get; set; }
        public string ComCarMake { get; set; }
        public string ComCarModel { get; set; }
        public string ComCarYear { get; set; }
        public string ComDamagePoints { get; set; }
        public string CINote { get; set; }
        public string TPLNote { get; set; }
        public string TotalLossNote { get; set; }
        public string HINote { get; set; }
        public string ComAccidentNote { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string ContractNotes { get; set; }
        public double? ATCElabour { get; set; }
        public double? ATCEsparePart { get; set; }
        public double? ATCEValueLoss { get; set; }
        public double? ATCETortCompensation { get; set; }
        public double? ATElabour { get; set; }
        public double? ATEsparePart { get; set; }
        public double? ATEValueLoss { get; set; }
        public double? ATETortCompensation { get; set; }
        public bool? TPReserves { get; set; }
        public double? ThirdTeamReserve1 { get; set; }
        public double? ThirdTeamReserve2 { get; set; }
        public double? ThirdTeamReserve3 { get; set; }
        public double? ThirdTeamReserve4 { get; set; }
        public double? ThirdTeamReserve5 { get; set; }
        public double? BodilyHarm1 { get; set; }
        public double? BodilyHarm2 { get; set; }
        public double? BodilyHarm3 { get; set; }
        public double? BodilyHarm4 { get; set; }
        public double? BodilyHarm5 { get; set; }
        public double? CPReserve { get; set; }
        public bool? COMReserves { get; set; }

    }
}
