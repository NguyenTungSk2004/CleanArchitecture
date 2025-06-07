using Base.Application.Service.Core.ValueObjects;
using HaiphongTech.Domain.Exceptions;

namespace HaiphongTech.Domain.Rules;

public static class ProductRules
{
    public static void EnsureValidPriceTiers(PriceTier priceTier1, PriceTier? priceTier2, PriceTier? priceTier3, PriceTier? priceTier4)
    {
        // Tier 1 bắt buộc
        if (priceTier1.Quantity <= 0 || priceTier1.Price <= 0)
            throw new BusinessRuleViolationException("Bậc giá 1 phải có số lượng và giá > 0");

        if (priceTier2 != null)
        {
            if (priceTier2.Quantity <= priceTier1.Quantity)
                throw new BusinessRuleViolationException("Số lượng bậc giá 2 phải lớn hơn bậc 1");
            if (priceTier2.Price >= priceTier1.Price)
                throw new BusinessRuleViolationException("Giá bậc 2 phải thấp hơn bậc 1");
        }

        if (priceTier3 != null)
        {
            var prev = priceTier2 ?? priceTier1;
            if (priceTier3.Quantity <= prev.Quantity)
                throw new BusinessRuleViolationException("Số lượng bậc giá 3 phải lớn hơn bậc trước");
            if (priceTier3.Price >= prev.Price)
                throw new BusinessRuleViolationException("Giá bậc 3 phải thấp hơn bậc trước");
        }

        if (priceTier4 != null)
        {
            var prev = priceTier3 ?? priceTier2 ?? priceTier1;
            if (priceTier4.Quantity <= prev.Quantity)
                throw new BusinessRuleViolationException("Số lượng bậc giá 4 phải lớn hơn bậc trước");
            if (priceTier4.Price >= prev.Price)
                throw new BusinessRuleViolationException("Giá bậc 4 phải thấp hơn bậc trước");
        }
    }
}
