public interface ICaveBoardPlotter<TResult> {
    void applyOn(CaveBoard caveBoard);
    TResult result();
}
