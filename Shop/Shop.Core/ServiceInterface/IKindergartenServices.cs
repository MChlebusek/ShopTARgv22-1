using Shop.Core.Domain;
using Shop.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface IKindergartenServices
    {
        Task<KinderGarten> Create(KinderGartenDto dto);

        Task<KinderGarten> GetAsync(Guid id);

        Task<KinderGarten> Update(KinderGartenDto dto);

        Task<KinderGarten> Delete(Guid id);
    }
}