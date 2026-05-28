
/**
 * Test file using jest to run it "npm test -- server/test.js" on root path
 */
import ApiWorker from "./api/worker.js"; 

// password match checker
test("comparePasswords should return false if password dont match", async () =>
{
    let hash = await ApiWorker.hashPassword("wrongPass1");
    let match = await ApiWorker.comparePasswords("wrongPass2", hash);
    expect(match).toBe(false);
});

// check wether the token type is still valid
test("generateToken should return a hex string of correct length",async () =>
{
    let token = await ApiWorker.createUniqueToken(100 / 2 , false);
    expect(typeof token).toBe("string");
    expect(token.length).toBeGreaterThan(0);
    // asked chatgpt to make the match regex
    expect(token).toMatch(/^[a-f0-9]+$/);
});


// Check if the data checker fail as intended (missing field)
test("AssertJsonRequirement should fail if a required field is missing",async () =>
{
    let fields = { objId: "url", required: true, wantedType: "string" };
    let result = await ApiWorker.fieldValidator(fields, {});
    expect(result.valid).toBe(false);
});

// Check if the data checker fail as intended (type match is wrong)
test("AssertJsonRequirement should fail if field type is wrong",async () =>
{
    let fields = { objId: "id", required: true, wantedType: "int" };
    let result = await ApiWorker.fieldValidator(fields, { id: "NaN" });
    expect(result.valid).toBe(false);
});