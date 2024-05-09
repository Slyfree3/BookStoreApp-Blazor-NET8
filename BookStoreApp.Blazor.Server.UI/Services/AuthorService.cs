﻿using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient client;
        private readonly IMapper mapper;

        public AuthorService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Response<int>> CreateAuthor(AuthorCreateDto author)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();
                await client.AuthorsPOSTAsync(author);
            }
            catch (ApiException apiException)
            {
                response = ConvertApiException<int>(apiException);
            }
            return response;
        }

        public async Task<Response<List<AuthorReadOnlyDto>>> GetAuthors()
        {
            Response<List<AuthorReadOnlyDto>> response;

            try
            {
                await GetBearerToken();
                var data = await client.AuthorsAllAsync();
                response = new Response<List<AuthorReadOnlyDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiException<List<AuthorReadOnlyDto>>(apiException);
            }
            return response;
        }

        public async Task<Response<AuthorReadOnlyDto>> GetAuthors(int id)
        {
            Response<AuthorReadOnlyDto> response;

            try
            {
                await GetBearerToken();
                var data = await client.AuthorsGETAsync(id);
                response = new Response<AuthorReadOnlyDto>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiException<AuthorReadOnlyDto>(apiException);
            }
            return response;
        }

        public async Task<Response<int>> EditAuthor(int id, AuthorUpdateDto author)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();
                await client.AuthorsPUTAsync(id, author);
            }
            catch (ApiException apiException)
            {
                response = ConvertApiException<int>(apiException);
            }
            return response;
        }

        public async Task<Response<AuthorUpdateDto>> GetAuthorForUpdate(int id)
        {
            Response<AuthorUpdateDto> response;

            try
            {
                await GetBearerToken();
                var data = await client.AuthorsGETAsync(id);
                response = new Response<AuthorUpdateDto>
                {
                    Data = mapper.Map<AuthorUpdateDto>(data),
                    Success = true
                };
            }
            catch (ApiException apiException)
            {
                response = ConvertApiException<AuthorUpdateDto>(apiException);
            }
            
            return response;
        }
    }
}
