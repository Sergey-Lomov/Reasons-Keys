﻿using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.DataModels
{
    internal class BranchPoint
    {
        internal static readonly int undefineBranch = -1;

        private const int hashMult = 13;
        private const int hashInit = 17;

        internal readonly int branch = 0;
        internal readonly int point = 0;

        public BranchPoint(int branch, int point)
        {
            this.branch = branch;
            this.point = point;
        }

        public override string ToString()
        {
            if (point == 0)
                return "-";

            var sign = point > 0 ? "+" : "";
            return sign + point.ToString() + "(" + branch + ")";
        }

        public static bool operator ==(BranchPoint bp1, BranchPoint bp2)
        {
            return bp1.point == bp2.point && bp1.branch == bp2.branch;
        }

        public static bool operator !=(BranchPoint bp1, BranchPoint bp2)
        {
            return !(bp1 == bp2);
        }

        public override bool Equals(object obj)
        {
            if (obj is BranchPoint)
                return this == (obj as BranchPoint);

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = hashInit;
                hash = hash * hashMult + branch.GetHashCode();
                hash = hash * hashMult + point.GetHashCode();
                return hash;
            }
        }
    }

    internal class BranchPointsSet
    {
        private const int hashMult = 19;
        private const int hashInit = 23;

        internal readonly List<BranchPoint> success = new List<BranchPoint>();
        internal readonly List<BranchPoint> failed = new List<BranchPoint>();

        public override string ToString()
        {
            var str = success.Count() > 0 ? "" : "-";
            foreach (var bp in success)
                str += bp.ToString() + " ";

            str += failed.Count() > 0 ? "/ " : "/ -";
            foreach (var bp in failed)
                str += bp.ToString() + " ";

            return str;
        }

        public List<BranchPoint> All()
        {
            var result = new List<BranchPoint>(success.Count() + failed.Count());
            result.AddRange(success);
            result.AddRange(failed);
            return result;
        }

        public BranchPointsSet(List<BranchPoint> success, List<BranchPoint> failed)
        {
            this.success = success ?? this.success;
            this.failed = failed ?? this.failed;
        }

        public static bool operator ==(BranchPointsSet bps1, BranchPointsSet bps2)
        {
            bool sameSuccess = Enumerable.SequenceEqual(bps1.success, bps2.success);
            bool sameFailed = Enumerable.SequenceEqual(bps1.failed, bps2.failed);
            return sameSuccess && sameFailed;
        }

        public static bool operator !=(BranchPointsSet bps1, BranchPointsSet bps2)
        {
            return !(bps1 == bps2);
        }

        public bool ContainsBranchPoint(BranchPoint branchPoint)
        {
            var filteredSuccess = success.Where(bp => bp == branchPoint);
            var filteredFailed = failed.Where(bp => bp == branchPoint);
            return filteredSuccess.Count() > 0 || filteredFailed.Count() > 0;
        }

        public BranchPointsTemplate Template()
        {
            var templateSucces = success.Select(bp => bp.point).ToArray();
            var templateFailed = failed.Select(bp => bp.point).ToArray();
            return new BranchPointsTemplate(templateSucces, templateFailed);
        }

        public BranchPointsSet Filter(int[] availableBranches)
        {
            var filteredSuccess = success.Where(bp => availableBranches.Contains(bp.branch)).ToList();
            var filteredFailed = failed.Where(bp => availableBranches.Contains(bp.branch)).ToList();
            return new BranchPointsSet(filteredSuccess, filteredFailed);
        }

        public bool ValidOnBranches(int[] availableBranches)
        {
            var invalidSuccess = success.Where(bp => !availableBranches.Contains(bp.branch));
            var invalidFailed = failed.Where(bp => !availableBranches.Contains(bp.branch));
            return invalidSuccess.Count() == 0 && invalidFailed.Count() == 0;
        }

        public HashSet<int> UsedBranches()
        {
            var branches = new HashSet<int>();
            branches.UnionWith(success.Select(bp => bp.branch));
            branches.UnionWith(failed.Select(bp => bp.branch));
            return branches;
        }

        public bool HasPositivePoints()
        {
            var positive = All().Where(bp => bp.point > 0);
            return positive.Count() > 0;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = hashInit;

                foreach (var bp in success)
                    hash = hash * hashMult + bp.GetHashCode();

                foreach (var bp in failed)
                    hash = hash * hashMult + bp.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is BranchPointsSet)
                return this == (obj as BranchPointsSet);

            return false;
        }
    }

    internal class BranchPointsTemplate
    {
        private const int hashMult = 19;
        private const int hashInit = 23;

        internal int[] success;
        internal int[] failed;

        public BranchPointsTemplate(int[] success, int[] failed)
        {
            this.success = success;
            this.failed = failed;
        }

        public BranchPointsSet SetupByBranches(int[] branches)
        {
            if (success.Count() + failed.Count() > branches.Count())
                return null;
            
            var bpSuccess = new List<BranchPoint> ();
            var bpFailed = new List<BranchPoint>();

            foreach (var point in success)
            {
                var branch = branches[bpSuccess.Count()];
                var branchPoint = new BranchPoint(branch, point);
                bpSuccess.Add(branchPoint);
            }

            foreach (var point in failed)
            {
                var branch = branches[bpSuccess.Count() + bpFailed.Count()];
                var branchPoint = new BranchPoint(branch, point);
                bpFailed.Add(branchPoint);
            }

            return new BranchPointsSet(bpSuccess, bpFailed);
        }

        public static bool operator ==(BranchPointsTemplate bps1, BranchPointsTemplate bps2)
        {
            bool sameSuccess = Enumerable.SequenceEqual(bps1.success, bps2.success);
            bool sameFailed = Enumerable.SequenceEqual(bps1.failed, bps2.failed);
            return sameSuccess && sameFailed;
        }

        public static bool operator !=(BranchPointsTemplate bps1, BranchPointsTemplate bps2)
        {
            return !(bps1 == bps2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = hashInit;

                foreach (var bp in success)
                    hash = hash * hashMult + bp.GetHashCode();

                foreach (var bp in failed)
                    hash = hash * hashMult + bp.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is BranchPointsTemplate)
                return this == (obj as BranchPointsTemplate);

            return false;
        }
    }

}
