<?php
header("Content-Type: application/json");

$host = 'localhost';
$db = 'books'; // Change this to your actual database name
$user = 'root';
$pass = ''; // Change this to your actual database password
$charset = 'utf8mb4';

$dsn = "mysql:host=$host;dbname=$db;charset=$charset";
$options = [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
    PDO::ATTR_EMULATE_PREPARES => false,
];

$pdo = new PDO($dsn, $user, $pass, $options);

if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    $stmt = $pdo->query("SELECT BookID, Title, PublishedDate, Genre, AuthorID FROM books");
    $books = $stmt->fetchAll();
    echo json_encode($books);
} elseif ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $input = json_decode(file_get_contents('php://input'), true);

    if (isset($input['title'], $input['publishedDate'], $input['genre'], $input['authorID'])) {
        $sql = "INSERT INTO books (Title, PublishedDate, Genre, AuthorID) VALUES (?, ?, ?, ?)";
        $stmt = $pdo->prepare($sql);
        $stmt->execute([
            $input['title'], 
            $input['publishedDate'], 
            $input['genre'], 
            $input['authorID']
        ]);
        echo json_encode(['message' => 'Book added successfully']);
    } else {
        echo json_encode(['error' => 'Invalid input']);
    }
}
?>
