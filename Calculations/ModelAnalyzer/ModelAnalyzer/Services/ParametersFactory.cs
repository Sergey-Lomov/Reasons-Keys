using ModelAnalyzer.Services;

using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Activities.EventsRestoring;
using ModelAnalyzer.Parameters.BranchPoints;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Events.Weight;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Moving;
using ModelAnalyzer.Parameters.Timing;
using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.Parameters.Predictability;
using ModelAnalyzer.Parameters.Topology;

using ModelAnalyzer.Parameters.Items;
using ModelAnalyzer.Parameters.Items.Standard;
using ModelAnalyzer.Parameters.Items.Standard.BaseShield;
using ModelAnalyzer.Parameters.Items.Standard.BaseWeapon;
using ModelAnalyzer.Parameters.Items.Standard.SpeedBooster;
using ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator;

using ModelAnalyzer.Parameters.Items.Artifacts;
using ModelAnalyzer.Parameters.Items.Artifacts.CoagulationGenerator;
using ModelAnalyzer.Parameters.Items.Artifacts.HoleBox;
using ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle;
using ModelAnalyzer.Parameters.Items.Artifacts.SymmetricalStabiliser;
using ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule;
using ModelAnalyzer.Parameters.Items.Artifacts.FateRavel;
using ModelAnalyzer.Parameters.Items.Standard.RelationsImprover;

namespace ModelAnalyzer
{
    class ModelFactory
    {
        public void LoadModel (Storage storage)
        {
            storage.AddParameter(parameter: new MaxPlayersAmount());
            storage.AddParameter(parameter: new MinPlayersAmount());

            // Activities
            storage.AddParameter(parameter: new EstimatedUnluckyStackChance());
            storage.AddParameter(parameter: new EventRestoringStackSize());
            storage.AddParameter(parameter: new MinimalLuckyStackChance());
            storage.AddParameter(parameter: new RealLuckyStackChance());
            storage.AddParameter(parameter: new RealUnluckyStackChance());

            storage.AddParameter(parameter: new ArtifactDischargeCoef());
            storage.AddParameter(parameter: new ArtifactsActingAmount());
            storage.AddParameter(parameter: new ArtifactsDischargeCompensation());
            storage.AddParameter(parameter: new AtackAmount());
            storage.AddParameter(parameter: new AUPriceProportion());
            storage.AddParameter(parameter: new EstimatedEventsConcreteBranchPoints());
            storage.AddParameter(parameter: new AverageUnkeyEventsConcreteBranchPoints());
            storage.AddParameter(parameter: new DestructionCoef());
            storage.AddParameter(parameter: new EstimatedEventCreationPrice());
            storage.AddParameter(parameter: new EstimatedEventImpactPrice());
            storage.AddParameter(parameter: new EUPartyAmount());
            storage.AddParameter(parameter: new EventCreationAmount());
            storage.AddParameter(parameter: new EventCreationPrice());
            storage.AddParameter(parameter: new EventCreationPriceAU());
            storage.AddParameter(parameter: new EventCreationPriceEU());
            storage.AddParameter(parameter: new EventImpactAmount());
            storage.AddParameter(parameter: new EventImpactPrice());
            storage.AddParameter(parameter: new EventImpactPriceAU());
            storage.AddParameter(parameter: new EventImpactPriceEU());
            storage.AddParameter(parameter: new EventImpact2PriceAU());
            storage.AddParameter(parameter: new EventImpact2PriceEU());
            storage.AddParameter(parameter: new EventImpact3PriceAU());
            storage.AddParameter(parameter: new EventImpact3PriceEU());
            storage.AddParameter(parameter: new EventsActionsPotential());
            storage.AddParameter(parameter: new InitialEventCreationAmount());
            storage.AddParameter(parameter: new KeyEventCreationAmount());
            storage.AddParameter(parameter: new MiningAmount());
            storage.AddParameter(parameter: new TotalPotential());
            storage.AddParameter(parameter: new NokeyEventCreationAmount());

            // BranchPoints
            storage.AddParameter(parameter: new BPTrackLength());
            storage.AddParameter(parameter: new BranchPointProfit());
            storage.AddParameter(parameter: new EstimatedGameBP());
            storage.AddParameter(parameter: new EstimatedPrognosedRealisationChance());
            storage.AddParameter(parameter: new InitialBP());
            storage.AddParameter(parameter: new MaxBPLoosingCoefficient());
            storage.AddParameter(parameter: new MaxGameBP());

            // Events
            storage.AddParameter(parameter: new MainDeck());
            storage.AddParameter(parameter: new MainDeckCore());

            storage.AddParameter(parameter: new ArtifactsRarity());
            storage.AddParameter(parameter: new AverageContinuumBP());
            storage.AddParameter(parameter: new AverageMiningBonus());
            storage.AddParameter(parameter: new AverageNozeroMiningBonus());
            storage.AddParameter(parameter: new AverageRelationsImpactPerCount());
            storage.AddParameter(parameter: new AverageRelationsImpactPower());
            storage.AddParameter(parameter: new BrachPointsTemplatesAllocation());
            storage.AddParameter(parameter: new BranchPointsAllocation_Standard());
            storage.AddParameter(parameter: new BranchPointsAllocation_Symmetric());
            storage.AddParameter(parameter: new BranchPointsDisbalance());
            storage.AddParameter(parameter: new BranchPointsRandomizationThreading());
            storage.AddParameter(parameter: new BranchPointsRandomizationLimit());
            storage.AddParameter(parameter: new EventMaxMiningBonus());
            storage.AddParameter(parameter: new EventMaxRelations());
            storage.AddParameter(parameter: new EventMiningBonusAllocation());
            storage.AddParameter(parameter: new EventMinMiningBonus());
            storage.AddParameter(parameter: new FrontEventsCoef());
            storage.AddParameter(parameter: new MinBackRelations());
            storage.AddParameter(parameter: new MinigForBalanceAverageStability());
            storage.AddParameter(parameter: new MinRelationsTemplateUsability());
            storage.AddParameter(parameter: new RelationImpactPower());
            storage.AddParameter(parameter: new RelationTemplatesUsage());

            // Events weight
            storage.AddParameter(parameter: new BackRelationsWeightCoef());
            storage.AddParameter(parameter: new EventUsabilityNormalisation());
            storage.AddParameter(parameter: new FrontRelationsWeightCoef());
            storage.AddParameter(parameter: new MiningBonusWeight());

            // Mining
            storage.AddParameter(parameter: new AverageMining());
            storage.AddParameter(parameter: new CenterMiningBonus());
            storage.AddParameter(parameter: new MiningAllocation());
            storage.AddParameter(parameter: new MiningAUCoef());
            storage.AddParameter(parameter: new MiningIncrement());

            // Moving
            storage.AddParameter(parameter: new InitialSpeed());
            storage.AddParameter(parameter: new InitialSpeedCoef());
            storage.AddParameter(parameter: new MotionAmount());
            storage.AddParameter(parameter: new MotionFreeLevel());
            storage.AddParameter(parameter: new MotionPrice());
            storage.AddParameter(parameter: new SpeedDoublingRate()); 
            storage.AddParameter(parameter: new OneRoundSpeedDoublingProfit());

            // Timing
            storage.AddParameter(parameter: new AUMoveAmount());
            storage.AddParameter(parameter: new AUPartyAmount());
            storage.AddParameter(parameter: new FirstRoundAttackers());
            storage.AddParameter(parameter: new MoveDuration());
            storage.AddParameter(parameter: new PartyDuration());
            storage.AddParameter(parameter: new PhasesAmount());
            storage.AddParameter(parameter: new PhasesDuration());
            storage.AddParameter(parameter: new PhasesWeight());
            storage.AddParameter(parameter: new RealisationDuration());
            storage.AddParameter(parameter: new RoundAmount());
            storage.AddParameter(parameter: new SafePeriodDuration());

            // Topology
            storage.AddParameter(parameter: new AverageDistance());
            storage.AddParameter(parameter: new AveragePhasesDistance());
            storage.AddParameter(parameter: new ContinuumFrontBlockersMap());
            storage.AddParameter(parameter: new ContinuumNodesAmount());
            storage.AddParameter(parameter: new FieldRadius());
            storage.AddParameter(parameter: new FrontBlockersMap());
            storage.AddParameter(parameter: new FrontBlockersRelativeMap());
            storage.AddParameter(parameter: new FrontImpactMap());
            storage.AddParameter(parameter: new MinDistancesPairsAmount_AA());
            storage.AddParameter(parameter: new NodesAvailableBackRelations());
            storage.AddParameter(parameter: new RoundNodesAmount());
            storage.AddParameter(parameter: new RoutesMap());
            storage.AddParameter(parameter: new StartFrontBlockersMap());

            // Player initial state
            storage.AddParameter(parameter: new AtackInitialEventMaxRadius());
            storage.AddParameter(parameter: new InitialEU());
            storage.AddParameter(parameter: new InitialEventsWeightCoefficient());
            storage.AddParameter(parameter: new InitialStackArtifactChance());
            storage.AddParameter(parameter: new InitialStackSize());
            storage.AddParameter(parameter: new KeyEventsAmount());
            storage.AddParameter(parameter: new KeyEventsBranchPoints());
            storage.AddParameter(parameter: new KeyEventsBranchPointsCoefficient());
            storage.AddParameter(parameter: new KeyEventsTotalBrachPoints());
            storage.AddParameter(parameter: new LogisticInitialEventMaxRelationPower());
            storage.AddParameter(parameter: new LogisticInitialEventMaxRadius());
            storage.AddParameter(parameter: new LogisticInitialEventPowerCoefficient());
            storage.AddParameter(parameter: new LogisticInitialEventTotalPower());
            storage.AddParameter(parameter: new MainKeyEventBranchPoints());
            storage.AddParameter(parameter: new MainKeyEventBranchPointsCoefficient());
            storage.AddParameter(parameter: new MinInitialCardUsability());
            storage.AddParameter(parameter: new RealInitialStackArtifactChance());
            storage.AddParameter(parameter: new RealKeyEventBrachPointCoefficients());
            storage.AddParameter(parameter: new StartDeck());
            storage.AddParameter(parameter: new SupportInitialEventMaxRadius());

            // Predictability
            storage.AddParameter(parameter: new AverageActrionsRoundProfit());
            storage.AddParameter(parameter: new AverageRoundProfit());
            storage.AddParameter(parameter: new LastRoundMaxProfit());
            storage.AddParameter(parameter: new LastRoundMaxProfitCoefficient());
            storage.AddParameter(parameter: new MaxBaseItemsRoundPureProfit());
            storage.AddParameter(parameter: new RoundMaxProfit());
            storage.AddParameter(parameter: new RoundMaxProfitCoefficient());

            // Items
            storage.AddParameter(parameter: new ArtifactsAvailabilityRound());
            storage.AddParameter(parameter: new ArtifactsAvaliabilityPhase());
            storage.AddParameter(parameter: new ArtifactsProfitCoefficient());
            storage.AddParameter(parameter: new AverageBaseItemsProfit());
            storage.AddParameter(parameter: new AveragePrimeItemsProfit());
            storage.AddParameter(parameter: new BaseItemsAmount());
            storage.AddParameter(parameter: new EstimatedArtifactsProfit());
            storage.AddParameter(parameter: new FullLoadCoefficient());
            storage.AddParameter(parameter: new ItemPerRoundLimit());
            storage.AddParameter(parameter: new ItemPriceCoefficient());
            storage.AddParameter(parameter: new ProtectionActualityCoefficient());
            storage.AddParameter(parameter: new PureEUProfitCoefficient());
            storage.AddParameter(parameter: new WeaponStandardEffectivity());
            storage.AddParameter(parameter: new WeaponStandardPower());

            // Standard
            storage.AddParameter(parameter: new EstimatedBaseItemRoundProfit());

            storage.AddParameter(parameter: new BS_BasePower());
            storage.AddParameter(parameter: new BS_Defense());
            storage.AddParameter(parameter: new BS_FullPrice());
            storage.AddParameter(parameter: new BS_MaxPower());
            storage.AddParameter(parameter: new BS_Profit());
            storage.AddParameter(parameter: new BS_UpgradesProfit());

            storage.AddParameter(parameter: new BW_Damage());
            storage.AddParameter(parameter: new BW_FullPrice());
            storage.AddParameter(parameter: new BW_MaxEffectivityCoefficient());
            storage.AddParameter(parameter: new BW_MaxPowerCoefficient());
            storage.AddParameter(parameter: new BW_OneUsagePureProfit());
            storage.AddParameter(parameter: new BW_Profit());
            storage.AddParameter(parameter: new BW_ShotPrice());
            storage.AddParameter(parameter: new BW_UpgradesAmount());
            storage.AddParameter(parameter: new BW_UpgradesProfit());

            storage.AddParameter(parameter: new KA_Capacity());
            storage.AddParameter(parameter: new KA_CapacityCoefficient());
            storage.AddParameter(parameter: new KA_FullPrice());
            storage.AddParameter(parameter: new KA_InversePower());
            storage.AddParameter(parameter: new KA_Profit());
            storage.AddParameter(parameter: new KA_RelativeRevenue());

            storage.AddParameter(parameter: new RI_ChargesAmount());
            storage.AddParameter(parameter: new RI_FullPrice());
            storage.AddParameter(parameter: new RI_ImprovementLimit());
            storage.AddParameter(parameter: new RI_ImprovementLimitCoefficient());
            storage.AddParameter(parameter: new RI_OneUsagePureProfit());
            storage.AddParameter(parameter: new RI_Profit());
            storage.AddParameter(parameter: new RI_RoundUsageLimit());
            storage.AddParameter(parameter: new RI_StandardPurchaseCount());

            storage.AddParameter(parameter: new SB_FullPrice());
            storage.AddParameter(parameter: new SB_MaxSpeedCoef());
            storage.AddParameter(parameter: new SB_Power());
            storage.AddParameter(parameter: new SB_Profit());
            storage.AddParameter(parameter: new SB_UpgradesAmount());
            storage.AddParameter(parameter: new SB_UpgradesProfit());

            // Artifacts
            storage.AddParameter(parameter: new AverageArtifactRoundProfit());

            storage.AddParameter(parameter: new CG_ChargesAmount());
            storage.AddParameter(parameter: new CG_OneUsageProfit());
            storage.AddParameter(parameter: new CG_Profit());
            storage.AddParameter(parameter: new CG_V616Mining());

            storage.AddParameter(parameter: new HB_CollapsePreparationDuration());
            storage.AddParameter(parameter: new HB_EstimatedOneUsageProfit());
            storage.AddParameter(parameter: new HB_MaxTension());
            storage.AddParameter(parameter: new HB_MaxTransaction());
            storage.AddParameter(parameter: new HB_Profit());
            storage.AddParameter(parameter: new HB_TensionIncreasing());
            storage.AddParameter(parameter: new HB_TensionInreasingStepsAmount());
            storage.AddParameter(parameter: new HB_TensionLimits());

            storage.AddParameter(parameter: new FR_ImpactSurcharge());
            storage.AddParameter(parameter: new FR_OwnerBets());
            storage.AddParameter(parameter: new FR_Profit());

            storage.AddParameter(parameter: new LN_ConnectionsAmount());
            storage.AddParameter(parameter: new LN_MaxRadius());
            storage.AddParameter(parameter: new LN_MinRadius());
            storage.AddParameter(parameter: new LN_OneConnectionProfit());
            storage.AddParameter(parameter: new LN_Profit());
            storage.AddParameter(parameter: new LN_Range());

            storage.AddParameter(parameter: new SS_Profit());
            storage.AddParameter(parameter: new SS_SecondaryImpactPower());
            storage.AddParameter(parameter: new SS_ImpactPower());
            storage.AddParameter(parameter: new SS_OneUsageProfit());
            storage.AddParameter(parameter: new SS_SymmetricalNodesAmount());
            storage.AddParameter(parameter: new SS_UsageAmount());

            storage.AddParameter(parameter: new CM_MaxLimit());
            storage.AddParameter(parameter: new CM_MaxLimitCoef());
            storage.AddParameter(parameter: new CM_MinLimit());
            storage.AddParameter(parameter: new CM_MinLimitCoef());
            storage.AddParameter(parameter: new CM_OneUsageProfit());
            storage.AddParameter(parameter: new CM_Profit());
            storage.AddParameter(parameter: new CM_UsageAmount());
            storage.AddParameter(parameter: new CM_UsageDifficulty());
        }
    }
}
