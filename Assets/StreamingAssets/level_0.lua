function degreesToRadians(degrees)
    return degrees * math.pi / 180
end

function circleSpawn(radius, object)
    for angle = 30, 360, 30 do
        local radians = degreesToRadians(angle)
        local x = radius * math.cos(radians)
        local y = radius * math.sin(radians)
        SpawnLevelPiece(object, Vector3(x, 1.5, y), Quaternion.Euler(0,0,0))
    end
end

circleSpawn(10, 'Goal')

SpawnLevelPiece('Stage', Vector3(15,0,-10), Quaternion.Euler(0,0,0))
SpawnLevelPiece('Stairs', Vector3(10,0,-10), Quaternion.Euler(0,0,0))
SpawnLevelPiece('Goal', Vector3(12.5,3.5,-12.5), Quaternion.Euler(0,0,0))

SpawnLevelPiece('Stage', Vector3(-10,0,-15), Quaternion.Euler(0,90,0))
SpawnLevelPiece('Stairs', Vector3(-10,0,-10), Quaternion.Euler(0,90,0))
SpawnLevelPiece('Goal', Vector3(-12.5,3.5,12.5), Quaternion.Euler(0,90,0))

SpawnLevelPiece('Stage', Vector3(-15,0,10), Quaternion.Euler(0,180,0))
SpawnLevelPiece('Stairs', Vector3(-10,0,10), Quaternion.Euler(0,180,0))
SpawnLevelPiece('Goal', Vector3(-12.5,3.5,-12.5), Quaternion.Euler(0,180,0))

SpawnLevelPiece('Stage', Vector3(10,0,15), Quaternion.Euler(0,-90,0))
SpawnLevelPiece('Stairs', Vector3(10,0,10), Quaternion.Euler(0,-90,0))
SpawnLevelPiece('Goal', Vector3(12.5,3.5,12.5), Quaternion.Euler(0,-90,0))
