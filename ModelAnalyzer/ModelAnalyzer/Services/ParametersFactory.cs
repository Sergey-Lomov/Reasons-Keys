using ModelAnalyzer.Parameters;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Events.Weight;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Moving;
using ModelAnalyzer.Parameters.Timing;
using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Items;
using ModelAnalyzer.Parameters.Items.Standard.BaseShield;
using ModelAnalyzer.Parameters.Items.Standard.BaseWeapon;

namespace ModelAnalyzer
{
    class ParametersFactory
    {
        public void LoadModel (Storage storage)
        {
            storage.AddParameter(parameter: new MaxPlayersAmount());
            storage.AddParameter(parameter: new MinPlayersAmount());

            // Activities
            storage.AddParameter(parameter: new AUPriceProportion());
            storage.AddParameter(parameter: new AverageEventsConcreteBranchPoints());
            storage.AddParameter(parameter: new AverageUnkeyEventsConcreteBranchPoints());
            storage.AddParameter(parameter: new DestructionCoef());
            storage.AddParameter(parameter: new EUPartyAmount());
            storage.AddParameter(parameter: new EventActionsCoef());
            storage.AddParameter(parameter: new EventCreationAmount());
            storage.AddParameter(parameter: new EventCreationPrice());
            storage.AddParameter(parameter: new EventCreationPriceAU());
            storage.AddParameter(parameter: new EventCreationPriceEU());
            storage.AddParameter(parameter: new EventImpactAmount());
            storage.AddParameter(parameter: new EventImpactPrice());
            storage.AddParameter(parameter: new EventImpactPriceAU());
            storage.AddParameter(parameter: new EventImpactPriceEU());
            storage.AddParameter(parameter: new EventsActionsPotential());
            storage.AddParameter(parameter: new KeyEventCreationAmount());
            storage.AddParameter(parameter: new StandardAtackAmount());
            storage.AddParameter(parameter: new UnkeyEventCreationAmount());
            storage.AddParameter(parameter: new TotalPotential());

            // Events
            storage.AddParameter(parameter: new MainDeck());

            storage.AddParameter(parameter: new ArtifactsAvaliabilityPhase());
            storage.AddParameter(parameter: new ArtifactsRarity());
            storage.AddParameter(parameter: new AverageMiningBonus());
            storage.AddParameter(parameter: new AverageStabilityIncrement());
            storage.AddParameter(parameter: new BlockEventsCoef_2D());
            storage.AddParameter(parameter: new BlockRelationsCoef_OB());
            storage.AddParameter(parameter: new BrachPointsTemplatesAllocation());
            storage.AddParameter(parameter: new BranchPointsAllocation_Standard());
            storage.AddParameter(parameter: new BranchPointsAllocation_Symmetric());
            storage.AddParameter(parameter: new ContinuumNodesAmount());
            storage.AddParameter(parameter: new EventMaxMiningBonus());
            storage.AddParameter(parameter: new EventMaxRelations());
            storage.AddParameter(parameter: new EventMiningBonusAllocation());
            storage.AddParameter(parameter: new EventMiningBonusConstraint());
            storage.AddParameter(parameter: new EventMinMiningBonus());
            storage.AddParameter(parameter: new FrontRelationsCoef());
            storage.AddParameter(parameter: new MinBackRelations()); 
            storage.AddParameter(parameter: new MultyblockCardsAllocation_OB());
            storage.AddParameter(parameter: new Pairing2Coef());
            storage.AddParameter(parameter: new Pairing3Coef());
            storage.AddParameter(parameter: new RelationsAmountAllocation_2D());
            storage.AddParameter(parameter: new RelationsAmountAllocation_OB());
            storage.AddParameter(parameter: new StabilityIncrementAllocation());

            // Events weight
            storage.AddParameter(parameter: new AdditionalReasonsWeight());
            storage.AddParameter(parameter: new ArtifactsWeight());
            storage.AddParameter(parameter: new AverageContinuumEventWeight());
            storage.AddParameter(parameter: new AverageRelationStability());
            storage.AddParameter(parameter: new AverageSequenceLength());
            storage.AddParameter(parameter: new BaseRelationsWeight());
            storage.AddParameter(parameter: new EventUsabilityNormalisation());
            storage.AddParameter(parameter: new FrontBlockerWeight());
            storage.AddParameter(parameter: new FrontReasonsWeight());
            storage.AddParameter(parameter: new MiningBonusWeight());
            storage.AddParameter(parameter: new NodesAvailableBackRelations());

            // Mining
            storage.AddParameter(parameter: new AverageMining());
            storage.AddParameter(parameter: new CenterMiningBonus());
            storage.AddParameter(parameter: new MiningAllocation());
            storage.AddParameter(parameter: new MiningAUCoef());
            storage.AddParameter(parameter: new MiningIncrement());

            // Moving
            storage.AddParameter(parameter: new InitialSpeed());
            storage.AddParameter(parameter: new InitialSpeedCoef());
            storage.AddParameter(parameter: new MaxSpeedCoef());
            storage.AddParameter(parameter: new MotionAmount());
            storage.AddParameter(parameter: new MotionFreeLevel());
            storage.AddParameter(parameter: new MotionPrice());
            storage.AddParameter(parameter: new SpeedDoublingRate());

            // Timing
            storage.AddParameter(parameter: new AUMoveAmount());
            storage.AddParameter(parameter: new AUPartyAmount());
            storage.AddParameter(parameter: new MoveDuration());
            storage.AddParameter(parameter: new PartyDuration());
            storage.AddParameter(parameter: new PhasesAmount());
            storage.AddParameter(parameter: new PhasesDuration());
            storage.AddParameter(parameter: new PhasesWeight());
            storage.AddParameter(parameter: new RealisationDuration());
            storage.AddParameter(parameter: new RoundAmount());

            // Topology
            storage.AddParameter(parameter: new AverageDistance());
            storage.AddParameter(parameter: new AveragePhasesDistance());
            storage.AddParameter(parameter: new FieldRadius());
            storage.AddParameter(parameter: new RoutesMap());
            storage.AddParameter(parameter: new MinDistancesPairsAmount());

            // Player initial state
            storage.AddParameter(parameter: new AverageInitialEventsBranchPoints());
            storage.AddParameter(parameter: new InitialEventsWeightCoefficient());
            storage.AddParameter(parameter: new KeyChainLenghtCoefficient());
            storage.AddParameter(parameter: new KeyEventsAmount());
            storage.AddParameter(parameter: new KeyEventsBranchPoints());
            storage.AddParameter(parameter: new KeyEventsBranchPointsCoefficient());
            storage.AddParameter(parameter: new KeyEventsTotalBrachPoints());
            storage.AddParameter(parameter: new LogisticsInitialCardCoefficient());
            storage.AddParameter(parameter: new MainKeyEventBranchPoints());
            storage.AddParameter(parameter: new MainKeyEventBranchPointsCoefficient());
            storage.AddParameter(parameter: new MiningInitialCardCoefficient());
            storage.AddParameter(parameter: new MinInitialCardUsability());
            storage.AddParameter(parameter: new RealKeyEventBrachPointCoefficients());
            storage.AddParameter(parameter: new StabilityInitialCardCoefficient());
            storage.AddParameter(parameter: new StartDeck());

            // Items
            storage.AddParameter(parameter: new ArtifactsEstimatedProfit());
            storage.AddParameter(parameter: new ArtifactsProfitCoefficient());
            storage.AddParameter(parameter: new AverageBaseItemsProfit());
            storage.AddParameter(parameter: new ItemPriceCoefficient());
            storage.AddParameter(parameter: new PureEUProfitCoefficient());
            storage.AddParameter(parameter: new WeaponStandardEffectivity());
            storage.AddParameter(parameter: new WeaponStandardPower());

            // Standard
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
            storage.AddParameter(parameter: new BW_Profit());
            storage.AddParameter(parameter: new BW_ShotPrice());
            storage.AddParameter(parameter: new BW_UpgradesAmount());
            storage.AddParameter(parameter: new BW_UpgradesProfit());
        }
    }
}
