using CameraCenterBe.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace TestChangeTrackingInCameraCenterBe.Apis;

public class MyTest
{
    public List<string> ListUserCamerasTest()
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                List<SqlUser> sql_users = context.users!
                                                .Include(s => s.role)
                                                .Include(s => s.customer)
                                                .Include(s => s.userGroups).ThenInclude(s => s.group).ThenInclude(s => s!.customer)
                                                .Include(s => s.userGroups).ThenInclude(s => s.group).ThenInclude(s => s!.groupCameras).ThenInclude(s => s.camera).ThenInclude(s => s!.customer)
                                                //.Include(s => s.userGroups).ThenInclude(s => s.group).ThenInclude(s => s!.groupCameras).ThenInclude(s => s.camera).ThenInclude(s => s!.cameraUsers)
                                                //.Include(s => s.userGroups).ThenInclude(s => s.group).ThenInclude(s => s!.groupCameras).ThenInclude(s => s.camera).ThenInclude(s => s!.cameraUsers).ThenInclude(s => s.user)
                                                .Include(s => s.cameraUsers).ThenInclude(s => s.camera).ThenInclude(s => s!.cameraUsers).ThenInclude(s => s.user)
                                                .AsNoTracking()
                                                .ToList();
                List<string> respondes = new List<string>();
                foreach (SqlUser f_user in sql_users)
                {
                    foreach (SqlUserGroup f_userGroup in f_user.userGroups)
                    {
                        if (f_userGroup.group == null)
                        {
                            continue;
                        }
                        foreach (SqlGroupCamera f_groupCamera in f_userGroup.group.groupCameras)
                        {
                            if (f_groupCamera.camera == null)
                            {
                                continue;
                            }
                            foreach (SqlCameraUser f_cameraUser in f_groupCamera.camera.cameraUsers)
                            {
                                if (f_cameraUser.user == null)
                                {
                                    continue;
                                }
                                string user = f_cameraUser.user.code;
                                respondes.Add(user);
                            }
                        }
                    }
                }
                return respondes;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new List<string>();
        }
    }

    public List<string> ListCamerasCamerasTest()
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                List<SqlCamera> sql_cameras = context.cameras!
                    .Include(s => s.groupCameras).ThenInclude(s => s.group).ThenInclude(s => s!.userGroups).ThenInclude(s => s.user).ThenInclude(s => s!.cameraUsers).ThenInclude(s => s.user)
                    //.AsNoTracking()
                    .ToList();
                List<string> respondes = new List<string>();
                foreach (SqlCamera f_camera in sql_cameras)
                {
                    if (f_camera.isDeleted)
                    {
                        continue;
                    }
                    foreach (SqlGroupCamera f_groupCamera in f_camera.groupCameras)
                    {
                        if (f_groupCamera.group == null)
                        {
                            continue;
                        }
                        foreach (SqlUserGroup f_userGroup in f_groupCamera.group.userGroups)
                        {
                            if (f_userGroup.user == null)
                            {
                                continue;
                            }
                            foreach (SqlCameraUser f_cameraUser in f_userGroup.user.cameraUsers)
                            {
                                if (f_cameraUser.camera == null)
                                {
                                    continue;
                                }
                                respondes.Add(f_cameraUser.camera.code);
                            }
                        }
                    }
                }
                return respondes;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new List<string>();
        }
    }
}
