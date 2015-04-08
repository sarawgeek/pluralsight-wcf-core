﻿using GeoLib.Contracts;
using GeoLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.Services
{
    public class GeoManager:IGeoService     
    {

        public GeoManager()
        {

        }

        public GeoManager(IStateRepository stateRepository)
        {
            _StateRepository = stateRepository;
        }
        public GeoManager(IZipCodeRepository zipCodeRepository)
        {
            _ZipCodeRepository = zipCodeRepository;
        }

        public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
        {
            _StateRepository = stateRepository;
            _ZipCodeRepository = zipCodeRepository;
        }

        IZipCodeRepository _ZipCodeRepository = null;
        IStateRepository _StateRepository = null;

        public ZipCodeData GetZipInfo(string zip)
        {
            ZipCodeData zipCodeData = null;
            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();
            ZipCode zipCodeEntity = zipCodeRepository.GetByZip(zip);
            if (zipCodeEntity != null)
            {
                zipCodeData = new ZipCodeData
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State.Abbreviation,
                    ZipCode = zipCodeEntity.Zip
                };
            }

            return zipCodeData;
        }

        public IEnumerable<string> GetStates(bool primaryStates)
        {
            List<string> stateData = new List<string>();
            IStateRepository stateRepository = _StateRepository?? new StateRepository();
            IEnumerable<State> states = stateRepository.Get(primaryOnly:true);
            if (states!=null)
            {
                foreach (State state in states)
                {
                    stateData.Add(state.Abbreviation);
                }
            }
            return stateData;
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();
            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();
            var zips = zipCodeRepository.GetByState(state);
            if (zips!=null)
            {
                foreach (ZipCode zipCode in zips)
                {
                    zipCodeData.Add(new ZipCodeData() 
                    {
                        City=zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode=zipCode.Zip
                    });
                }
               
            }

            return zipCodeData;
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();
            IZipCodeRepository zipCodeRepository = new ZipCodeRepository();
            ZipCode zipEntity = zipCodeRepository.GetByZip(zip);
            var zips = zipCodeRepository.GetZipsForRange(zipEntity, range);
            if (zips!=null)
            {
                foreach (ZipCode zipCode in zips)
                {
                    zipCodeData.Add(new ZipCodeData() 
                    {
                        City= zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode=zipCode.Zip
                    });
                }
            }
            return zipCodeData;
        }
    }
}
