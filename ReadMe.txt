********************************* The SEVEN MUSIC RATER APP *********************************

Table of Contents:

1. Concept
2. Structure
	A )Artist - Table 1 & 2
	B) Album - Table 3 & 4
	C) Song - Table 5 & 6
	D) Store - Table 7 & 8
3. Future Features
4. Local Instructions
5. Links to requirements 


********************************************************************************************
---------------------------------------1. Concept-------------------------------------------

********************************************************************************************

The Seven Music Rater App has been developed for users to rate their favorite Artists, Albums, Songs, and Music Stores. Additionally to that, the App bring users additionally functionality by relating all Artists to their Albums, Albums to its Songs, and Music Stores to which an Album may be available. The final feature is the "Community Rating" feature. This allows each users to log in and rate any of the aforementioned categories (Artist, Album, Song, Store). All ratings are then averaged to give an averaged score.


********************************************************************************************
--------------------------------------2.Structure-------------------------------------------

********************************************************************************************


As seen in the wireframe document, the data has been structured as follows: 


-------------A) Artist ------------------------------
Table 1 - Artist:
-ArtistId(Key)
-ArtistName
-ArtistRating (community rating. Total Score / Number of Reviews)
-TotalScore (not displayed to user, populated from ArtistRating.IndividualArtistRating)
- NumberOfReviews (not displayed to user, populated from ArtistRating.IndividualArtistRating)

Table 2 Artist Rating (Subset of Artist):
- ArtistRatingId (Key)
- IndividualArtistRating 
- ArtistId(Foreign Key) 

-------------B) Album ------------------------------
Table 3 - Album 
-AlbumId (Key)
-AlbumName 
-ArtistId (Foreign Key)
-AlbumRating (community rating. Total Score / Number of Reviews)
-TotalScore (not displayed to user, populated from AlbumRating.IndividualAlbumRating)
- NumberOfReviews (not displayed to user, populated from AlbumRating.IndividualAlbumRating)

Table 4 Album Rating (Subset of Album):
- AlbumRatingId (Key)
- IndividualAlbumRating 
- AlbumId(Foreign Key) 

-------------C) Song ------------------------------
Table 3 - Song
-SongId (Key)
-SongName 
-AlbumId (Foreign Key)
-SongRating (community rating. Total Score / Number of Reviews)
-TotalScore (not displayed to user, populated from SongRating.IndividualSongRating)
- NumberOfReviews (not displayed to user, populated from SongRating.IndividualSongRating)

Table 4 Song Rating (Subset of Song):
- SongRatingId (Key)
- IndividualSongRating 
- SongId(Foreign Key) 

-------------D) Store ------------------------------
Table 3 - Store
-StoreId (Key)
-StoreName 
-AlbumId (Foreign Key)
-StoreRating (community rating. Total Score / Number of Reviews)
-TotalScore (not displayed to user, populated from StoreRating.IndividualStoreRating)
- NumberOfReviews (not displayed to user, populated from StoreRating.IndividualStoreRating)

Table 4 Store Rating (Subset of Store):
- StoreRatingId (Key)
- IndividualStoreRating 
- StoreId(Foreign Key) 

********************************************************************************************
-----------------------------------3. Future Features---------------------------------------

********************************************************************************************

Some of the features we would like to add in the future:
- Store: Album Catalogue. Allow the user to view a list of albums in the store
- Front End UI
- Sort by: Artist, Top Ratings, etc. 

********************************************************************************************
-----------------------------------4. Local Instructions------------------------------------

********************************************************************************************


1) Account Register
POST - Register Account
URL: https://localhost:*****/api/Account/Register

Key 		Value
Email		email@email.com
Password 	(enter password here)
ConfirmPassword (confirm your password!)


2) LOG IN
POST - Log In to get bearer token 
URL: https://localhost:****/Token

Key 		Value
grant_type	password
username	(email from Token request)
passwrod	(password from Token request)

3) All Tables Instruction:
For All Categories (Artist, Album, Song, Store):
URL for main table: https://localhost:****/api/[Category]
URL for get by ID: https://localhost:****/api/[Category]/[ID#]
URL for ratings table: https://localhost:*****/api/[Category]Rating 

4) Artist- CRUD 
	
	Create: POST 
	Required: ArtistName


	Read: GET
	Required: Nothing
	Displayed: List of: ArtistId, ArtistName, ArtistRating

	Read: GetByID
	Required: ArtistID, GetByID URL (see above)
	DisplayeD: ArtistId, ArtistName, ArtistRating

	Update: PUT
	Required: ArtistId, ArtistName 

	Delete: DELETE
	Required: ArtistId, GetByID URL (see above)

5) ArtistRating - CRUD 

	Create: POST 
	Required: ArtistId, ArtistIndividualRating

	Read: GET
	Required: Nothing
	Displayed: List of: ArtistRatingId, ArtistIndividualRating, ArtistId

	Read: GetByID
	Required: ArtistRatingID, GetByID URL (see above)
	Displayed: ArtistRatingId, ArtistIndividualRating, ArtistId

	Update: PUT
	Required: ArtistRatingId, ArtistIndividualRating, ArtistId

	Delete: DELETE
	Required: ArtistRatingId, GetByID URL (see above)

		
6) Album - CRUD 
	
	Create: POST 
	Required: AlbumName
	Optional: ArtistId(foreignkey)


	Read: GET
	Required: Nothing
	Displayed: List of: AlbumId, AlbumName, AlbumRating, (optional) ArtistId

	Read: GetByID
	Required: AlbumID, GetByID URL (see above)
	DisplayeD: AlbumId, AlbumName, AlbumRating, (optional) ArtistId

	Update: PUT
	Required: AlbumId, AlbumName, (optional) ArtistId

	Delete: DELETE
	Required: AblumId, GetByID URL (see above)

7) AlbumRating - CRUD 

	Create: POST 
	Required: AlbumId, AlbumIndividualRating

	Read: GET
	Required: Nothing
	Displayed: List of: AlbumRatingId, AlbumIndividualRating, AlbumId

	Read: GetByID
	Required: AlbumRatingID, GetByID URL (see above)
	Displayed: AlbumRatingId, AlbumIndividualRating, AlbumId

	Update: PUT
	Required: AlbumRatingId, AlbumIndividualRating, AlbumId

	Delete: DELETE
	Required: AlbumRatingId, GetByID URL (see above)	

8) Song - CRUD 
	
	Create: POST 
	Required: SongName
	Optional: AlbumId(foreignkey)


	Read: GET
	Required: Nothing
	Displayed: List of: SongId, SongName, SongRating, (optional) AlbumId

	Read: GetByID
	Required: SongId, GetByID URL (see above)
	DisplayeD: SongId, SongName, SongRating, (optional) AlbumId

	Update: PUT
	Required: SongId, SongName, (optional) AlbumId

	Delete: DELETE
	Required: SongId, GetByID URL (see above)

9) SongRating - CRUD 

	Create: POST 
	Required: SongId, SongIndividualRating

	Read: GET
	Required: Nothing
	Displayed: List of: SongRatingId, SongIndividualRating, SongId

	Read: GetByID
	Required: SongRatingID, GetByID URL (see above)
	Displayed: SongRatingId, SongIndividualRating, SongId

	Update: PUT
	Required: SongRatingId, SongIndividualRating, SongId

	Delete: DELETE
	Required: SongRatingId, GetByID URL (see above)	

10) Store- CRUD 
	
	Create: POST 
	Required: StoreName
	Optional: AlbumId(foreignkey)


	Read: GET
	Required: Nothing
	Displayed: List of: StoreId, StoreName, SongRating, (optional) AlbumId, Address

	Read: GetByID
	Required: StoreId, GetByID URL (see above)
	DisplayeD: StoreId, StoreName, StoreRating, (optional) AlbumId

	Update: PUT
	Required: StoreId, StoreName, (optional) AlbumId

	Delete: DELETE
	Required: StoreId, GetByID URL (see above)

11) StoreRating - CRUD 

	Create: POST 
	Required: StoreId, StoreIndividualRating

	Read: GET
	Required: Nothing
	Displayed: List of: StoreRatingId, StoreIndividualRating, StoreId

	Read: GetByID
	Required: StoreRatingID, GetByID URL (see above)
	Displayed: StoreRatingId, StoreIndividualRating, StoreId

	Update: PUT
	Required: StoreRatingId, StoreIndividualRating, StoreId

	Delete: DELETE
	Required: StoreRatingId, GetByID URL (see above)	


********************************************************************************************
--------------------------------------4. Links -------------------------------------------

********************************************************************************************


Trello Board: https://trello.com/earthboundseven

