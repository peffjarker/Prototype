// Services/FranchiseService.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public interface IFranchiseService
    {
        IReadOnlyList<FranchiseOption> GetFranchises();
        FranchiseOption? FindByDealerId(string? dealerId);
    }

    public class FranchiseService : IFranchiseService
    {
        private readonly List<FranchiseOption> _franchises = new()
        {
            new("A1B2", "10001", "Alex",   "Meyer"),
            new("C3D4", "20002", "Jordan", "Price"),
            new("E5F6", "30003", "Sam",    "Carter"),
            new("G7H8", "40004", "Taylor", "Nguyen"),
        };

        public IReadOnlyList<FranchiseOption> GetFranchises() => _franchises;

        public FranchiseOption? FindByDealerId(string? dealerId)
        {
            if (string.IsNullOrWhiteSpace(dealerId))
                return null;

            return _franchises.FirstOrDefault(f =>
                string.Equals(f.DealerId, dealerId, StringComparison.OrdinalIgnoreCase));
        }
    }

    public sealed record FranchiseOption(
        string DealerId,
        string ClientKey,
        string FirstName,
        string LastName)
    {
        public string Display => $"{DealerId} — {FirstName} {LastName}";

        // For sidebar display
        public string ShortDisplay => $"{DealerId} • {FirstName} {LastName}";
    }
}