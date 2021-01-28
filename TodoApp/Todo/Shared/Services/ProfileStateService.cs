using Fistix.Training.Domain.Commands.Profiles;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Todo.Shared.Models;

namespace Todo.Shared.Services
{
  public class ProfileStateService
  {
    private readonly HttpClient _httpClient = null;
    private readonly AuthHandler _authHandler = null;

    public ProfileStateService(HttpClient httpClient, AuthHandler authHandler)
    {
      _httpClient = httpClient;
      _authHandler = authHandler;

      GetMyProfileDetail();

    }

    private BehaviorSubject<ProfileDto> _profileSubject = new BehaviorSubject<ProfileDto>(new ProfileDto());
    private Subject<ApiCallResult> _apiCallResultSubject = new Subject<ApiCallResult>();

    public IObservable<ProfileDto> ProfileObservable { get { return _profileSubject; } }
    public IObservable<ApiCallResult> ApiCallResultObservable { get { return _apiCallResultSubject; } }


    public async void GetMyProfileDetail()
    {
      await GetMyProfile();
    }
    public async void UpdateMyProfile(UpdateMyProfileCommand command)
    {
      await Update(command);
    }
    private async Task Update(UpdateMyProfileCommand command)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());

        var response = await _httpClient.PutAsJsonAsync<UpdateMyProfileCommand>("api/Profiles/MyProfile", command);
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<UpdateMyProfileCommandResult>();

          var profile = new ProfileDto();

          //profile.ProfileId = commandResult.Payload.ProfileId;
          //profile.Email = commandResult.Payload.Email;
          profile.FirstName = commandResult.Payload.FirstName;
          profile.LastName = commandResult.Payload.LastName;
          //profile.ProfilePictureUrl = commandResult.Payload.ProfilePictureUrl;

          _profileSubject.OnNext(profile);
          _apiCallResultSubject.OnNext(new ApiCallResult()
          {
            IsSucceed = true,
            Operation = "UpdateMyProfile"
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult()
        {
          IsSucceed = false,
          Operation = "UpdateMyProfile",
          ErrorMessage = ex.Message
        });
      }
    }
    public async void UpdateMyProfilePicture(System.IO.Stream file, string fileName)//UpdateMyProfilePictureCommand command)
    {
      await UpdateMyPicture(file, fileName);
    }
    private async Task UpdateMyPicture(System.IO.Stream file, string fileName)//UpdateMyProfilePictureCommand command)
    {
      try
      {
        var formData = new MultipartFormDataContent();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());

        var content = new StreamContent(file); //command.ProfilePicture.OpenReadStream());
        formData.Add(content, nameof(UpdateMyProfilePictureCommand.ProfilePicture), fileName);// command.ProfilePicture.FileName);

        var response = await _httpClient.PutAsync("api/Profiles/MyProfile/ProfileImage", formData);
        if (response.IsSuccessStatusCode)
        {
          formData.Dispose();

          var commandResult = await response.Content.ReadFromJsonAsync<UpdateMyProfilePictureCommandResult>();

          var profile = new ProfileDto();

          profile.ProfilePictureUrl = commandResult.ProfilePictureUrl;

          _profileSubject.OnNext(profile);
          _apiCallResultSubject.OnNext(new ApiCallResult()
          {
            IsSucceed = true,
            Operation = "UpdateMyProfilePicture"
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult()
        {
          IsSucceed = false,
          Operation = "UpdateMyProfilePicture",
          ErrorMessage = ex.Message
        });
      }
    }

    private async Task GetMyProfile()
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var result = await _httpClient.GetFromJsonAsync<GetProfileDetailByEmailQueryResult>("api/Profiles/MyProfile");

        _profileSubject.OnNext(result.Payload);

        _apiCallResultSubject.OnNext(new ApiCallResult()
        {
          IsSucceed = true,
          Operation = "GetMyProfileDetail"
        });

      }
      catch (Exception ex)
      {

        _apiCallResultSubject.OnNext(new ApiCallResult()
        {
          IsSucceed = false,
          Operation = "GetMyProfileDetail",
          ErrorMessage = ex.Message
        });
      }

    }


  }
}
