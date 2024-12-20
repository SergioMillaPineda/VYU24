﻿using CountriesManagement.Infrastructure.Contracts.Entities;

namespace CountriesManagement.Infrastructure.Contracts
{
    public interface ICountryPopulationRepository
    {
        Task<AllCountriesPopulationEntity?> GetPopulationForAllCountries();
        Task<AllCountriesPopulationEntity> GetPopulationByCountryInitialAndYear(char initial, int year);
    }
}
