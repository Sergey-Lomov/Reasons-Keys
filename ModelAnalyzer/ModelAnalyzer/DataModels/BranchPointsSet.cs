using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.DataModels
{
    internal class BranchPoint
    {
        private const int hashMult = 13;
        private const int hashInit = 17;

        internal readonly int branch = 0;
        internal readonly int point = 0;

        public BranchPoint(int branch, int point)
        {
            this.branch = branch;
            this.point = point;
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

        public BranchPointsSet(List<BranchPoint> success, List<BranchPoint> failed)
        {
            this.success = success != null ? success : this.success;
            this.failed = failed != null ? failed : this.failed;
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
