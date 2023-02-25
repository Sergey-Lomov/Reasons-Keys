
namespace ModelAnalyzer.Parameters.Activities
{
    class ArtifactsActingAmount : FloatSingleParameter
    {
        public ArtifactsActingAmount()
        {
            type = ParameterType.In;
            title = "Оценка кол-ва разыгрываемых артефактов";
            details = "Задает оценку кол-ва артефактов разыгранных в течении партии всеми игроками (а не одним как в случае других похожих параметров)";
            fractionalDigits = 0;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
