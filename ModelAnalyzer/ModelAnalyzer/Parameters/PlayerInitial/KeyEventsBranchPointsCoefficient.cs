﻿namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class KeyEventsBranchPointsCoefficient : FloatSingleParameter
    {
        public KeyEventsBranchPointsCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. очков ветвей на решающих событиях";
            details = "Определяет отношение кол-ва очков ветви на решающих картах среднего игрока к кол-ву очков ветви этого игрока (как положительных так и отрицательных) на разыгрываемых в среднем за партию картах событий (континуума и изначальных). Это отражает смещение баланса между планомерной подготовкой своих решающих событий и необходимостью оперативно перемещаться по полю и  влиять на события других игроков, чтобы использовать победные очки с их карт.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
