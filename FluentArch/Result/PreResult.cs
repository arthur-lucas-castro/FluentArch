using FluentArch.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentArch.Result
{
    public class PreResult
    {
        private bool _isSuccessful;
        public bool IsSuccessful { get { return _isSuccessful; } }
        public IEnumerable<ViolationDto> _violacoes;

        public PreResult(bool isSuccessful)
        {
            _isSuccessful = isSuccessful;
            _violacoes = new List<ViolationDto>();
        }
        public PreResult(bool isSuccessful, IEnumerable<ViolationDto> violacoes)
        {
            _isSuccessful = isSuccessful;
            _violacoes = violacoes;
        }
    }
}
