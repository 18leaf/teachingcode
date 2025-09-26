-- must have workspaces folder "Cutscnes", part named 1, 2, ..., n (add S for skip frame)
-- also have Trigger part in workspace to activate the cutscene
local tweenservice = game:GetService("TweenService")

local plr = game.Players.LocalPlayer

local currentCamera = workspace.CurrentCamera
local folder = workspace:WaitForChild("Cutscene")

local db = true

local function cueCutsceneCam(speed)
 local tweeninfo = TweenInfo.new(speed, Enum.EasingStyle.Linear, Enum.EasingDirection.Out)

 currentCamera.CameraType = Enum.CameraType.Scriptable
 currentCamera.CFrame = folder[1].CFrame

 for i = 2, #folder:GetChildren() do
  if folder:FindFirstChild(i.."S") then
   currentCamera.CFrame = folder[i.."S"].CFrame
  else
   tweenservice:Create(currentCamera, tweeninfo, {CFrame = folder[i].CFrame}):Play()
   wait(tweeninfo.Time)
  end
 end

 currentCamera.CameraType = Enum.CameraType.Custom
end

workspace:WaitForChild("Trigger").Touched:Connect(function(touched)
 if touched.Parent == plr.Character and db == true then db = false
  cueCutsceneCam(3)
  wait(5)
  db = true
 end
end)