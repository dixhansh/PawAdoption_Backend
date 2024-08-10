﻿using AutoMapper;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;
using PawAdoption_Backend.Repositories;

namespace PawAdoption_Backend.Services
{
    public class PetService : IPetService
    {
        private readonly IMapper mapper;
        private readonly IPetRepository petRepository;

        public PetService(IMapper mapper,IPetRepository petRepository)
        {
            this.mapper = mapper;
            this.petRepository = petRepository;
        }

        public async Task<PetResponseDto> CreatePetAsync(CreatePetRequestDto petDto)
        {
            //dto to domainModel
            var pet = mapper.Map<Pet>(petDto);

            var newPet = await petRepository.AddPetAsync(pet);

            //domainModel to dto
            var newPetDto = mapper.Map<PetResponseDto>(newPet);

            return (newPetDto);
        }

     
        public async Task<MedicalRecordDto?> UpdateMedicalRecordAsync(Guid petId, MedicalRecordDto petMedicalRecordDto)
        {
            //finding the pet
            var existingPet = await petRepository.FindPetById(petId);
            if (existingPet != null)
            {
                //mapping dto to domain model
                var medicalRecord = mapper.Map<PetMedicalRecord>(petMedicalRecordDto);

                //assigning petid to medicalRecord
                medicalRecord.PetId = petId;

                //Creating new pet record if it does not exist
                if (existingPet.PetMedicalRecord == null)
                {
                    medicalRecord = await petRepository.CreateMedicalRecordAsync(medicalRecord);
                }
                //updating medicalRecord of existingPet
                if(existingPet.PetMedicalRecord != null)
                {
                    medicalRecord = await petRepository.RenewMedicalRecordAsync(medicalRecord, existingPet.PetMedicalRecord.Id);
                }
                //mapping model back to dto
                petMedicalRecordDto = mapper.Map<MedicalRecordDto>(medicalRecord);
                return (petMedicalRecordDto);
            }
            return (null);
        }

        public async Task<PetResponseDto?> GetByIdAsync(Guid id)
        {
            var pet = await petRepository.FindPetById(id);
            if(pet != null)
            {
                return mapper.Map<PetResponseDto>(pet);
            }

            return null;
        }

        public async Task<List<PetResponseDto>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            var petList = await petRepository.GetAllFromDbAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            
            return (mapper.Map<List<PetResponseDto>>(petList));

        }
    }
}