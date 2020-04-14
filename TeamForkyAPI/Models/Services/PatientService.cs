﻿using TeamForkyAPI.Data;
using TeamForkyAPI.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamForkyAPI.DTOs;
using System.Linq;

namespace TeamForkyAPI.Models.Services
{
    //Inherit from IPatient
    public class PatientService : IPatients
    {
        //gain access to table properties
        private HospitalDbContext _context { get; }

        public PatientService(HospitalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a patient
        /// </summary>
        /// <param name="patient">patient object</param>
        /// <returns></returns>
        public async Task CreatePatient(Patient patient)
        {
            _context.Add(patient);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns a list of patients from table
        /// </summary>
        /// <returns></returns>
        public async Task<List<PatientsDTO>> GetAllPatients()
        {
            List<Patient> patients = await _context.Patient.ToListAsync();
            List<PatientsDTO> pDTO = new List<PatientsDTO>();
            foreach (var patient in patients)
            {
                PatientsDTO PDTO = ConvertToDTO(patient);
                pDTO.Add(PDTO);
            }
            return pDTO;
        }

        /// <summary>
        /// Get patient by ID
        /// </summary>
        /// <param name="ID">int</param>
        /// <returns></returns>
        public async Task<PatientsDTO> GetPatientByID(int ID)
        {
            var patient = await _context.Patient.FindAsync(ID);
            PatientsDTO pDTO = ConvertToDTO(patient);

            var patientResources = await _context.PatientResources.Where(x => x.PatientID == ID)
                                            .Include(x => x.Resources)
                                            .ToListAsync();
       
            List<ResourcesDTO> patientRes = new List<ResourcesDTO>();

            foreach (var pr in patientResources)
            {     
                ResourcesDTO rdto = new ResourcesDTO
                {
                    ResourcesType = pr.Resources.ResourcesType.ToString(),
                    Name = pr.Resources.Name,
                    Description = pr.Resources.Description
                };
                patientRes.Add(rdto);
            }
            pDTO.Resources = patientRes;
            return pDTO;
        }

        /// <summary>
        /// Delete a Patient from Database
        /// </summary>
        /// <param name="ID"> Patient ID </param>
        public async Task RemovePatient(int ID)
        {
            Patient patient = await _context.Patient.FindAsync(ID);

            _context.Patient.Remove(patient);    
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update Patient by ID TODO but cant include status or it will break
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        public async Task UpdatePatient(int ID, Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Convert To DTOs
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public PatientsDTO ConvertToDTO(Patient patient)
        {
            PatientsDTO pDTO = new PatientsDTO()
            {
                ID = patient.ID,
                Name = patient.Name,
                Birthday = patient.Birthday,
                CheckIn = patient.CheckIn,
                Status = patient.Status.ToString()
            };
            return pDTO;
        }
    }
}
