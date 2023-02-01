using Core.Specification;

namespace EcommerceAPI.Specification
{
    public class UserSpecification : BaseSpecification<Model.User>
    {
        public UserSpecification( UserSpecificationParams userParams)
            : base(x =>
            (string.IsNullOrEmpty(userParams.Search) || x.Name.Contains(userParams.Search))&&
            (string.IsNullOrEmpty(userParams.Name) || x.Name.Contains(userParams.Name)) &&
            (string.IsNullOrEmpty(userParams.LastName) ||x.LastName.Contains(userParams.LastName))
            ) 
        {
            ApplyPaging(userParams.PageSize * (userParams.PageSize - 1), userParams.PageSize);
            if (!string.IsNullOrEmpty(userParams.Sort))
            {
                switch (userParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(u => u.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(u => u.Name);
                        break;
                    case "emailAsc":
                        AddOrderBy(u => u.Email);
                        break;
                    case "emailDesc":
                        AddOrderByDescending(u => u.Email);
                        break;
                    case "usernameAsc":
                        AddOrderBy(u => u.UserName);
                        break;
                    case "usernameDsc":
                        AddOrderByDescending(u => u.UserName);
                        break;
                        default:
                        AddOrderBy(u =>u.Name);
                        break;

                }
            }
        }
    }
}
