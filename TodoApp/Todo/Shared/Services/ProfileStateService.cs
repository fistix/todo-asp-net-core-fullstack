using Fistix.Training.Domain.Commands.Profiles;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries.Profiles;
using System;
using System.Collections.Generic;
using System.IO;
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

      //
      GetAllProfiles();
    }

    private BehaviorSubject<ProfileDto> _myProfileBehaviorSubject = new BehaviorSubject<ProfileDto>(new ProfileDto());
    private Subject<ApiCallResult> _apiCallResultSubject = new Subject<ApiCallResult>();

    public IObservable<ProfileDto> MyProfileObservable { get { return _myProfileBehaviorSubject; } }
    public IObservable<ApiCallResult> ApiCallResultObservable { get { return _apiCallResultSubject; } }


    private BehaviorSubject<List<ProfileDto>> _allProfilesBeahiourSubject = new BehaviorSubject<List<ProfileDto>>(new List<ProfileDto>());
    public IObservable<List<ProfileDto>> AllProfilesObservable { get { return _allProfilesBeahiourSubject; } }


    public async void GetAllProfiles()
    {
      await GetAll();
    }

    private async Task GetAll()
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var result = await _httpClient.GetFromJsonAsync<GetAllProfilesQueryResult>("api/profiles");

        _allProfilesBeahiourSubject.OnNext(result.Payload);

        _apiCallResultSubject.OnNext(new ApiCallResult()
        {
          IsSucceed = true,
          Operation = "GetAllProfiles"
        });

      }
      catch (Exception ex)
      {

        _apiCallResultSubject.OnNext(new ApiCallResult()
        {
          IsSucceed = false,
          Operation = "GetAllProfiles",
          ErrorMessage = ex.Message
        });
      }

    }
    public async void GetMyProfileDetail()
    {
      await GetMyProfile();
    }
    
    public async void UpdateMyProfilePicture(Stream file, string fileName)//UpdateMyProfilePictureCommand command)
    {
      await UpdateMyPicture(file, fileName);
    }


    public async void UpdateMyProfile(UpdateMyProfileCommand command, System.IO.Stream file, string fileName)
    {
      if(file != null && fileName != null) 
      {
        var pictureResponse = await UpdateMyPicture(file, fileName);
        
        if (pictureResponse!=null && pictureResponse.IsSuccessStatusCode)
        {
          var profileResponse = await Update(command);
          if (profileResponse != null)
          {
            _myProfileBehaviorSubject.OnNext(profileResponse.Payload);
            _apiCallResultSubject.OnNext(new ApiCallResult()
            {
              IsSucceed = true,
              Operation = "UpdateMyProfile"
            });
          }
        }
      }
      
    }

    private async Task<UpdateMyProfileCommandResult> Update(UpdateMyProfileCommand command)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        UpdateMyProfileCommandResult commandResult = null;
        var response = await _httpClient.PutAsJsonAsync<UpdateMyProfileCommand>("api/Profiles/MyProfile", command);
        if (response.IsSuccessStatusCode)
        {
          commandResult = await response.Content.ReadFromJsonAsync<UpdateMyProfileCommandResult>();

          //var profile = new ProfileDto();

          ////profile.ProfileId = commandResult.Payload.ProfileId;
          ////profile.Email = commandResult.Payload.Email;
          //profile.FirstName = commandResult.Payload.FirstName;
          //profile.LastName = commandResult.Payload.LastName;
          ////profile.ProfilePictureUrl = commandResult.Payload.ProfilePictureUrl;

          //_profileSubject.OnNext(profile);
          //_apiCallResultSubject.OnNext(new ApiCallResult()
          //{
          //  IsSucceed = true,
          //  Operation = "UpdateMyProfile"
          //});

        }
        return commandResult;
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult()
        {
          IsSucceed = false,
          Operation = "UpdateMyProfile",
          ErrorMessage = ex.Message
        });
        return null;
      }
    }
  
    private async Task<HttpResponseMessage> UpdateMyPicture(System.IO.Stream file, string fileName)//UpdateMyProfilePictureCommand command)
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

          //var commandResult = await response.Content.ReadFromJsonAsync<UpdateMyProfilePictureCommandResult>();

          //var profile = new ProfileDto();

          //profile.ProfilePictureUrl = commandResult.ProfilePictureUrl;

          //_profileSubject.OnNext(profile);
          //_apiCallResultSubject.OnNext(new ApiCallResult()
          //{
          //  IsSucceed = true,
          //  Operation = "UpdateMyProfilePicture"
          //});

        }
        return response;
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult()
        {
          IsSucceed = false,
          Operation = "UpdateMyProfilePicture",
          ErrorMessage = ex.Message
        });
        return null;
      }
    }

    private async Task GetMyProfile()
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var result = await _httpClient.GetFromJsonAsync<GetProfileDetailByEmailQueryResult>("api/Profiles/MyProfile");

        _myProfileBehaviorSubject.OnNext(result.Payload);

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
