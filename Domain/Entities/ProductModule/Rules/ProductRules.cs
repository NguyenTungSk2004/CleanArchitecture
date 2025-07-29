using Domain.Entities.ProductModule.ValueObjects;
using Domain.Exceptions;

namespace Domain.Entities.ProductModule.Rules
{
    public static class ProductRules
    {
        public static void EnsureValidPriceTiers(List<PriceTier> priceTiers)
        {
            if (priceTiers == null || priceTiers.Count == 0)
                throw new BusinessRuleViolationException("Danh sách bậc giá không được để trống.");

            if (priceTiers.Count < 1 || priceTiers[0].Quantity <= 0 || priceTiers[0].Price <= 0)
                throw new BusinessRuleViolationException("Bậc giá 1 phải có số lượng và giá > 0");

            for (int i = 1; i < priceTiers.Count; i++)
            {
                var currentTier = priceTiers[i];
                var previousTier = priceTiers[i - 1];
                if (currentTier.Quantity <= 0 && currentTier.Price <= 0) continue;

                if (currentTier.Quantity <= previousTier.Quantity)
                    throw new BusinessRuleViolationException($"Số lượng bậc giá {i + 1} phải lớn hơn bậc {i}");
                if (currentTier.Price >= previousTier.Price)
                    throw new BusinessRuleViolationException($"Giá bậc {i + 1} phải thấp hơn bậc {i}");
            }
        }
    }

}

