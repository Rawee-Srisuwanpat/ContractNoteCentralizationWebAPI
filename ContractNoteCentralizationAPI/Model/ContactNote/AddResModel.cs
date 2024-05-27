using ContractNoteCentralizationAPI.Model.Common;

namespace ContractNoteCentralizationAPI.Model.ContactNote
{
    public class AddResModel
    {

        public AddResModel()
        {
            status = new StatusModel();
        }
        public StatusModel status { get; set; }

    }
}
